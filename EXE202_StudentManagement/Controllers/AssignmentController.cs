using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }


        [Route("Assignment/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var assignment=_assignmentService.GetAssignmentById(id);


            return View(assignment);

        }
    }
}
