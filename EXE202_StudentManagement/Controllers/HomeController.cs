using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Introduction()
        {
            ViewData["ActivePage"] = "Introduction";
            return View();
        }
    }
}
