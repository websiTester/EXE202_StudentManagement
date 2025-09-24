using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("h")]
public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IClassRepository2 _classRepo;

    public HomeController(UserManager<User> userManager, IClassRepository2 classRepo)
    {
        _userManager = userManager;
        _classRepo = classRepo;
    }

    [HttpGet("")]
    public IActionResult Index() => View();

    [HttpGet("introduction")]
    public async Task<IActionResult> Introduction()
    {
        ViewData["ActivePage"] = "Introduction";

        var model = new IntroductionViewModel
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            UserName = User.Identity?.Name
        };

        // Lấy userId theo Identity (safe)
        var userId = _userManager.GetUserId(User);

        if (!string.IsNullOrEmpty(userId))
        {
            // gọi repository để xác định teacher/student theo DB class/studentclass
            model.IsTeacher = await _classRepo.IsUserTeacherInAnyClassAsync(userId);
            model.IsStudent = await _classRepo.IsUserStudentInAnyClassAsync(userId);
        }

        return View(model);
    }
}
