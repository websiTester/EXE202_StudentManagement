using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
	[Authorize]
	public class PaymentController : Controller
	{
		private UserManager<User> _userManager;
		public PaymentController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> PaymentService()
		{
			PaymentServiceViewModel model = new PaymentServiceViewModel()
			{
				Username = User.Identity.Name
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> PayQR(PaymentServiceViewModel model)
		{

			return View(model);
		}
	}
}