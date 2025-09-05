using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class ErrorController : Controller
    {

        //Error404
        public IActionResult Error404()
        {
            return View();
        }
    }
}
