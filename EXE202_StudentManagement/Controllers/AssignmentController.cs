using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentService _assignmentService;
        IGroupService _groupService;
        IGroupTaskService _groupTaskService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AssignmentController(IAssignmentService assignmentService, IGroupService groupService, IGroupTaskService groupTaskService, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _assignmentService = assignmentService;
            _groupService = groupService;
            _groupTaskService = groupTaskService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Route("Assignment/Detail/{id}")]
        public async Task<IActionResult> DetailAsync(int id)
        {
            var assignment=_assignmentService.GetAssignmentById(id);
            User user = await _userManager.GetUserAsync(User);

            var myGroup = _groupService.GetGroupByMemberId(user.Id);

            if (assignment.IsGroupAssignment.GetValueOrDefault() && myGroup != null)
            {
                var submission = _assignmentService.GetSubmissionByGroup(id, myGroup.GroupId);
                ViewBag.Submission = submission;
            }
          
            ViewBag.MyGroup = myGroup;
            return View(assignment);

        }


        [HttpPost]
        [Route("api/Assignment/CreateTask")]
        public IActionResult CreateTask([FromBody] GroupTask newTask)
        {
            // Giờ bạn có thể truy cập newTask.AssignmentId và newTask.GroupId
            // Các giá trị này được gửi từ form

            _groupTaskService.AddGroupTask(newTask);

            return Ok(new { success = true, message = "Task created successfully." });
        }

        [HttpPost]
        [Route("api/GroupTask/UpdateStatus")]
        public IActionResult UpdateTaskStatus([FromBody] UpdateTaskStatusViewModel model)
        {
            if (model == null || model.TaskId <= 0 || string.IsNullOrEmpty(model.NewStatus))
            {
                return BadRequest(new { success = false, message = "Invalid data." });
            }

            _groupTaskService.UpdateGroupTask(model.TaskId, model.NewStatus);

            return Ok(new { success = true, message = "Task status updated successfully." });
        }

        [HttpDelete]
        [Route("api/GroupTask/Delete/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            var success = _groupTaskService.DeleteGroupTask(taskId);

            if (success)
            {
                return Ok(new { success = true, message = "Task deleted successfully." });
            }

            return NotFound(new { success = false, message = "Task not found." });
        }

        [HttpPost]
        [Route("api/Assignment/SubmitFile")]
        public async Task<IActionResult> SubmitFile(IFormFile file, int assignmentId)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            // Lấy thông tin nhóm của người dùng
            var myGroup = _groupService.GetGroupByMemberId(user.Id);
            if (myGroup == null)
            {
                return BadRequest(new { success = false, message = "Bạn chưa thuộc nhóm nào." });
            }
            var existingSubmission = _assignmentService.GetSubmissionByGroup(assignmentId, myGroup.GroupId);
            if (existingSubmission != null)
            {
                return BadRequest(new { success = false, message = "Nhóm của bạn đã nộp bài này rồi." });
            }





            if (file == null || file.Length == 0)
            {
                return BadRequest(new { success = false, message = "Vui lòng chọn một file để nộp." });
            }

            // Kiểm tra định dạng file
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".rar" && fileExtension != ".zip")
            {
                return BadRequest(new { success = false, message = "Định dạng file không hợp lệ. Vui lòng nộp file .rar hoặc .zip." });
            }

            try
            {
               
             

                // Tạo tên file duy nhất để tránh trùng lặp
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var folderPath = Path.Combine("wwwroot", "submissions"); // Thư mục lưu trữ file
                var filePath = Path.Combine(folderPath, fileName);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Lưu file vật lý vào thư mục trên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Tạo đối tượng Assignment_Submission và lưu vào DB
                var submission = new AssignmentSubmission
                {
                    AssignmentId = assignmentId,
                    StudentId = user.Id,
                    SubmitLink = $"/submissions/{fileName}", // Lưu đường dẫn tương đối
                    SubmittedAt = DateTime.Now
                };

             _assignmentService.AddSubmission(submission);

                    return Ok(new { success = true, message = "Nộp file thành công." });
              

            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return StatusCode(500, new { success = false, message = "Đã xảy ra lỗi server: " + ex.Message });
            }
        }


        [HttpPost]
        [Route("api/Assignment/SubmitLink")]
        public async Task<IActionResult> SubmitLink([FromBody] SubmitLinkViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            var myGroup = _groupService.GetGroupByMemberId(user.Id);
            if (myGroup == null)
            {
                return BadRequest(new { success = false, message = "Bạn chưa thuộc nhóm nào." });
            }

            var existingSubmission = _assignmentService.GetSubmissionByGroup(model.AssignmentId, myGroup.GroupId);
            if (existingSubmission != null)
            {
                return BadRequest(new { success = false, message = "Nhóm của bạn đã nộp bài này rồi." });
            }

            if (model == null || string.IsNullOrEmpty(model.SubmitLink))
            {
                return BadRequest(new { success = false, message = "Link nộp bài không được để trống." });
            }

            try
            {
              

                var submission = new AssignmentSubmission
                {
                    AssignmentId = model.AssignmentId,
                    StudentId = user.Id,
                    SubmitLink = model.SubmitLink,
                    SubmittedAt = DateTime.Now
                };

                _assignmentService.AddSubmission(submission);

               
                    return Ok(new { success = true, message = "Nộp bài thành công." });
             

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Đã xảy ra lỗi server: " + ex.Message });
            }
        }

        [HttpDelete]
[Route("api/Assignment/CancelSubmission/{assignmentId}")]
        public async Task<IActionResult> CancelSubmission(int assignmentId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            var myGroup = _groupService.GetGroupByMemberId(user.Id);
            if (myGroup == null)
            {
                return BadRequest(new { success = false, message = "Bạn chưa thuộc nhóm nào." });
            }

            var submission = _assignmentService.GetSubmissionByGroup(assignmentId, myGroup.GroupId);
            if (submission == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy bài nộp của nhóm để hủy." });
            }

            var result = _assignmentService.DeleteSubmission(assignmentId, submission.StudentId);

            if (result)
            {
                return Ok(new { success = true, message = "Hủy nộp bài thành công." });
            }

            return StatusCode(500, new { success = false, message = "Lỗi khi hủy nộp bài." });
        }







    }
}




