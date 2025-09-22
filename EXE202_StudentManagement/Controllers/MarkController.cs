using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EXE202_StudentManagement.Controllers
{
    public class MarkController : Controller
    {
        private readonly IGradingService _gradingService;
        public MarkController(IGradingService service)
        {
            this._gradingService = service;
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
        [ValidateAntiForgeryToken]
        public IActionResult SaveGrading(GradingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Nếu dữ liệu form không hợp lệ, cần load lại các thông tin không được post về
                // và trả lại view với dữ liệu người dùng đã nhập
                var reloadedModel = _gradingService.GetGradingDetails(model.AssignmentId, model.GroupId);
                if (reloadedModel == null) return NotFound();

                // Cập nhật lại model đã load với dữ liệu người dùng vừa nhập để không bị mất
                reloadedModel.GroupGrade = model.GroupGrade;
                reloadedModel.GroupComment = model.GroupComment;
                for (int i = 0; i < model.Members.Count; i++)
                {
                    var memberInDb = reloadedModel.Members.FirstOrDefault(m => m.StudentId == model.Members[i].StudentId);
                    if (memberInDb != null)
                    {
                        memberInDb.Grade = model.Members[i].Grade;
                        memberInDb.Comment = model.Members[i].Comment;
                    }
                }
                return View(reloadedModel);
            }

            var success = _gradingService.SaveGrading(model);

            if (success)
            {
                TempData["SuccessMessage"] = "Đã lưu đánh giá thành công!";
                // Chuyển hướng về trang chi tiết bài tập (ví dụ)
                return RedirectToAction("Details", "Assignments", new { id = model.AssignmentId });
            }

            ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra khi lưu đánh giá. Vui lòng thử lại.");
            // Cần load lại dữ liệu đầy đủ trước khi trả về view
            var finalModel = _gradingService.GetGradingDetails(model.AssignmentId, model.GroupId);
            return View(finalModel);
        }
    }
}
