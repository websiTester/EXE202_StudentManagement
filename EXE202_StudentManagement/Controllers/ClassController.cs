using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
	public class ClassController : Controller
	{
        private readonly IStudentClassService _studentClassService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ClassController(IStudentClassService studentClassService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _studentClassService = studentClassService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);

            IEnumerable<StudentClass> studentClasses = new List<StudentClass>();

            if (user != null)
            {
                string currentUserId = user.Id;
                studentClasses = _studentClassService.GetClassesByStudentId(currentUserId);
            }

            return View(studentClasses);
        }

        [HttpPost]
        [Route("api/Class/Join")]
        public async Task<IActionResult> Join([FromBody] JoinClassViewModel model)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Bạn cần đăng nhập để tham gia lớp." });
            }

            var result = _studentClassService.JoinClass(user.Id, model.ClassCode);

            if (result.success)
            {
                return Ok(new { success = true, message = result.message });
            }

            return BadRequest(new { success = false, message = result.message });
        }
    }
}
