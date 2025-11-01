using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class MaterialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
