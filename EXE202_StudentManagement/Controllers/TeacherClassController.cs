using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class TeacherClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<User> _userManager;
        public TeacherClassController(IClassService classService, ICourseRepository courseRepository, UserManager<User> userManager)
        {
            _classService = classService;
            _courseRepository = courseRepository;
            _userManager = userManager;
        }

        [Route("Teacher/Class/Home")]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            IEnumerable<Class> teacherClasses = new List<Class>();
            IEnumerable<Course> allCourses = _courseRepository.GetCoursesByTeacherId(user.Id);

            if (user != null)
            {
                teacherClasses = _classService.GetClassesByTeacherId(user.Id);
            }

            ViewBag.AllCourses = allCourses;
            ViewBag.CurrentUserId = user?.Id;
            return View(teacherClasses);
        }

        [HttpPost]
        [Route("api/Class/Create")]
        public async Task<IActionResult> Create([FromBody] CreateClassViewModel model)
        {
            User user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Bạn cần đăng nhập để tạo lớp." });
            }

            model.TeacherId = user.Id;
            var result = _classService.CreateNewClass(model);

            if (result.success)
            {
                return Ok(new { success = true, message = result.message });
            }

            return BadRequest(new { success = false, message = result.message });
        }


    }
}
