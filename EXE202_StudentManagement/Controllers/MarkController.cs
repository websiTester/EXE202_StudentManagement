using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace EXE202_StudentManagement.Controllers
{
    public class MarkController : Controller
    {
        private readonly IGradingService _gradingService;
        private readonly IAssignment2Service _assignment2Service;
        private readonly UserManager<User> _userManager;
        public MarkController(IGradingService service, UserManager<User> userManager, IAssignment2Service assignment2Service)
        {
            this._gradingService = service;
            this._userManager = userManager;
            this._assignment2Service = assignment2Service;
        }
        [Route("mark/grading/{assignmentId}/{groupId}")]
        public IActionResult GradingPage(int groupId, int assignmentId)
        {
            var viewModel = _gradingService.GetGradingDetails(assignmentId, groupId);
            if (viewModel == null)
            {
                return NotFound(); // Hoặc trả về một trang lỗi thân thiện
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveGroupGrade(GradingViewModel model)
        {
            ModelState.Remove(nameof(model.AssignmentName));
            ModelState.Remove(nameof(model.GroupName));
            ModelState.Remove(nameof(model.SubmissionLink));
            ModelState.Remove(nameof(model.Members));
            ModelState.Remove(nameof(model.GroupComment));
            if (ModelState.IsValid)
            {
                _gradingService.SaveGroupGrade(model);
                TempData["SuccessMessage"] = "Đã lưu đánh giá chung thành công!";
                return RedirectToAction("GradingPage", new { assignmentId = model.AssignmentId, groupId = model.GroupId });
            }
            
            var fullViewModel = _gradingService.GetGradingDetails(model.AssignmentId, model.GroupId);
            if (fullViewModel == null)
            {
                return NotFound();
            }
            // Giữ lại dữ liệu người dùng đã nhập
            fullViewModel.GroupGrade = model.GroupGrade;
            fullViewModel.GroupComment = model.GroupComment;

            return View("GradingPage", fullViewModel);
        }

        [HttpPost]
        public IActionResult SaveMemberGrades(GradingViewModel model)
        {
           
            // Lấy ID giáo viên từ User Identity
            var teacherId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(teacherId))
            {
                return Unauthorized(); // Hoặc xử lý lỗi phù hợp
            }

            // Bỏ qua kiểm tra các trường không được gửi từ form này
            ModelState.Remove(nameof(model.AssignmentName));
            ModelState.Remove(nameof(model.GroupName));
            ModelState.Remove(nameof(model.SubmissionLink));
            ModelState.Remove(nameof(model.GroupGrade));
            ModelState.Remove(nameof(model.GroupComment));
            if (model.Members != null)
            {
                for (int i = 0; i < model.Members.Count; i++)
                {
                    ModelState.Remove($"Members[{i}].FullName");
                    ModelState.Remove($"Members[{i}].Comment");
                }
            }

            if (ModelState.IsValid)
            {
                _gradingService.SaveMemberGrades(model, teacherId);
                TempData["SuccessMessage"] = "Đã lưu đánh giá thành viên thành công!";
                return RedirectToAction("GradingPage", new { assignmentId = model.AssignmentId, groupId = model.GroupId });
            }

            // Xử lý khi model không hợp lệ: Lấy chi tiết lỗi
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);

            var fullViewModel = _gradingService.GetGradingDetails(model.AssignmentId, model.GroupId);
            if (fullViewModel == null)
            {
                return NotFound();
            }
            // Giữ lại dữ liệu người dùng đã nhập cho từng thành viên
            for (int i = 0; i < fullViewModel.Members.Count; i++)
            {
                // Đảm bảo không truy cập index ngoài phạm vi
                if (model.Members != null && i < model.Members.Count)
                {
                    fullViewModel.Members[i].Grade = model.Members[i].Grade;
                    fullViewModel.Members[i].Comment = model.Members[i].Comment;
                }
            }

            return View("GradingPage", fullViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFileAsync(int assignmentId, int groupId)
        {
            try
            {
                // Lấy submission từ DB
                var submission = _gradingService.GetSubmissionLink(assignmentId,groupId);
                if (submission == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài nộp." });
                }

                // Đường dẫn vật lý của file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", submission.SubmitLink.TrimStart('/'));

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { success = false, message = "File không tồn tại trên server." });
                }

                // Lấy tên file gốc để hiển thị khi tải về (nếu bạn muốn có thể lưu FileName gốc trong DB)
                var fileName = Path.GetFileName(filePath);

                // Xác định content type (MIME)
                var contentType = "application/octet-stream"; // mặc định
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

                // Đọc file thành byte[]
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                // Trả file về cho browser tải xuống
                return File(fileBytes, contentType ?? "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }
        [HttpGet]
        [Route("mark/list/{classId}")]
        public IActionResult AssignmentList(int classId)
        {
            if (classId <= 0)
            {
                return BadRequest("Class ID không hợp lệ.");
            }

            try
            {
                var assignments =  _assignment2Service.GetAllClassAssignment(classId);
                // Truyền model (danh sách bài tập) vào View
                // View không cần thay đổi vì vẫn nhận cùng một kiểu model
                return View(assignments);
            }
            catch (System.Exception)
            {
                // TODO: Log the exception
                // Có thể chuyển hướng đến một trang lỗi chung
                return View("Error");
            }
        }
    }
}
