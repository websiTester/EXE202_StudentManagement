using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentService _assignmentService;
        IGroupService _groupService;
        IGroupTaskService _groupTaskService;

        public AssignmentController(IAssignmentService assignmentService, IGroupService groupService, IGroupTaskService groupTaskService)
        {
            _assignmentService = assignmentService;
            _groupService = groupService;
            _groupTaskService = groupTaskService;
        }

        [Route("Assignment/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var assignment=_assignmentService.GetAssignmentById(id);
            string currentUserId = "user1";

            var myGroup = _groupService.GetGroupByMemberId(currentUserId);
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




