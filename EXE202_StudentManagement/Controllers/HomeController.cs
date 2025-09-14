using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentHome()
        {
            ViewData["ActivePage"] = "Home";
            return View();
        }

        public IActionResult TeacherHome()
        {
            return View();
        }
    }
}
