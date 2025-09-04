using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
	public class ClassController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
