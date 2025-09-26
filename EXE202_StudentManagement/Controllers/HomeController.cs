using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IClassRepository2 _classRepo;

    public HomeController(UserManager<User> userManager, IClassRepository2 classRepo)
    {
        _userManager = userManager;
        _classRepo = classRepo;
    }

    public async Task<IActionResult> Introduction()
    {
        ViewData["ActivePage"] = "Introduction";

        var model = new IntroductionViewModel
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            UserName = User.Identity?.Name
        };

        return View(model);
    }

}
