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
            Console.WriteLine("NUMBER OF GROUPS: " + myGroup.StudentGroups.Count);
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
    }
}




