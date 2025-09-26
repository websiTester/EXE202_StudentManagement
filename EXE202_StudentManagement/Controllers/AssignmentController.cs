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
        IPeerReviewService _peerReviewService;

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AssignmentController(IAssignmentService assignmentService, 
            IGroupService groupService, IGroupTaskService groupTaskService, 
            IPeerReviewService peerReviewService, UserManager<User> userManager, 
            SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _assignmentService = assignmentService;
            _groupService = groupService;
            _groupTaskService = groupTaskService;
            _peerReviewService = peerReviewService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Route("Assignment/Detail/{id}")]
        public async Task<IActionResult> DetailAsync(int id)
        {
            var assignment=_assignmentService.GetAssignmentById(id);
            User user = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = user;

            var myGroup = _groupService.GetGroupByMemberId(user.Id, assignment.ClassId);

            if (assignment.IsGroupAssignment.GetValueOrDefault() && myGroup != null)
            {
                var submission = _assignmentService.GetSubmissionByGroup(id, myGroup.GroupId);
                ViewBag.Submission = submission;
            }
            if (myGroup != null)
            {
                var groupMembers = myGroup.StudentGroups
                                           .Where(sg => sg.StudentId != user.Id)
                                           .Select(sg => sg.Student)
                                           .ToList();
                ViewBag.GroupMembers = groupMembers;

                var myReviews = _peerReviewService.GetReviewsForMember(user.Id, id);
                ViewBag.MyReviews = myReviews;
            }





            ViewBag.MyGroup = myGroup;
            return View(assignment);

        }


        [HttpPost]
        [Route("api/Assignment/CreateTask")]
        public IActionResult CreateTask([FromBody] GroupTask newTask)
        {
          

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




        [HttpPost]
        [Route("api/Assignment/SubmitPeerReview")]
        public async Task<IActionResult> SubmitPeerReview([FromBody] PeerReviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            // Lấy bản đánh giá hiện có (nếu có)
            var existingReview = _peerReviewService.GetExistingReview(user.Id, model.RevieweeId, model.AssignmentId);
            bool result = false;

            if (existingReview != null)
            {
                // Nếu đã có đánh giá, cập nhật điểm và nhận xét
                existingReview.Score = model.Score;
                existingReview.Comment = model.Comment;
                existingReview.CreateAt = DateTime.Now;
                result = _peerReviewService.UpdatePeerReview(existingReview);
            }
            else
            {
                // Nếu chưa có, tạo mới
                var review = new PeerReview
                {
                    AssignmentId = model.AssignmentId,
                    GroupId = model.GroupId,
                    ReviewerId = user.Id,
                    RevieweeId = model.RevieweeId,
                    Comment = model.Comment,
                    Score = model.Score,
                    CreateAt = DateTime.Now
                };
                result = _peerReviewService.AddPeerReview(review);
            }

            if (result)
            {
                return Ok(new { success = true, message = "Đánh giá đã được gửi thành công." });
            }

            return StatusCode(500, new { success = false, message = "Lỗi khi lưu đánh giá." });
        }














    }








}





