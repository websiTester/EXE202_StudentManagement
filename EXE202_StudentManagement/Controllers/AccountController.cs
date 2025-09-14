using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
	public class AccountController : Controller
	{

		private UserManager<User> _userManager;
		private SignInManager<User> _signInManager;
		private RoleManager<IdentityRole> _roleManager;
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public async Task<IActionResult> SelectRoleBeforeLogin()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Login()
		{
			
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(
					model.Username, model.Password, model.RememberMe, false);
				
				if (result.Succeeded)
				{
					//User user = await _userManager.FindByNameAsync(model.Username);
					//User user = await _userManager.GetUserAsync(User);
					return View("Index");
				}
				else
				{		
					//Error will display in asp-validation-summary
					ModelState.AddModelError("", "Invalid username or password");
				}
			}
			
			return View(model);
		}


		[HttpGet]
		public IActionResult Register(string? role)
		{
			RegisterViewModel model = new RegisterViewModel();
			if(role != null)
			{
				model.Role = role;
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = new User()
				{
					UserName = model.Username,
					Email = model.Email
				};
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					ViewBag.ErrorTitle = "Registration successful";
					await _userManager.AddToRoleAsync(user, model.Role);
					
					return View("RegisterSuccess");
				}
				else
				{
					//Error will display in asp-validation-summary
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}

		public async Task<IActionResult> IsUsernameInUse(string Username)
		{
			var user = await _userManager.FindByNameAsync(Username);
			if (user == null)
			{
				return Json(true);
			}
			else
			{
				return Json($"Username {Username} is already in use");
			}
		}

		public async Task<IActionResult> IsEmailInUse(string Email)
		{
			var user = await _userManager.FindByEmailAsync(Email);
			if (user == null)
			{
				return Json(true);
			}
			else
			{
				return Json($"Email {Email} is already in use");
			}
		}
	}
}
