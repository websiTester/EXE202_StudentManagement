using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EXE202_StudentManagement.Controllers
{
    public class MarkController : Controller
    {
        private readonly ITeacherMarkService _service;
        private readonly IAssignment2Service _assignment2;
        public MarkController(ITeacherMarkService service, IAssignment2Service assignment2)
        {
            this._service = service;
            _assignment2 = assignment2;
        }

        public IActionResult TeacherMark(int groupId, int assignmentId)
        {
            var model = _service.GetGroupDetails(groupId, assignmentId);
            if(model == null) return NotFound();
            return View(model);
        }

        public IActionResult AssigmentList()
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(teacherId))
                return Unauthorized();

            var model =  _assignment2.GetAssignmentByTeacherID(teacherId);
            return View("AssigmentList", model);
        }
    }
}
