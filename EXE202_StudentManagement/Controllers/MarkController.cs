using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class MarkController : Controller
    {
        public IActionResult TeacherMark()
        {
            return View();
        }
    }
}
