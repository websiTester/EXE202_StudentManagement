using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentService _assignmentService;
        IGroupService _groupService;
public AssignmentController(IAssignmentService assignmentService, IGroupService groupService)
        {
            _assignmentService = assignmentService;
            _groupService = groupService;
        }


        [Route("Assignment/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var assignment=_assignmentService.GetAssignmentById(id);
            string currentUserId = "user4";

            var myGroup = _groupService.GetGroupByMemberId(currentUserId);
            ViewBag.MyGroup = myGroup;
            return View(assignment);

        }
    }
}
