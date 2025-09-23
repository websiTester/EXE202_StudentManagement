using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Controllers
{
    public class UserController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        private readonly IStatisticsService _statisticsService;
        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IStatisticsService statisticsService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _statisticsService = statisticsService;
        }
        [Route("admin/detail/{id}")]
        public async Task<IActionResult> UserDetailAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.isActive = false; // Khóa tài khoản

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUser", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("UserDetail", user);
        }

        [HttpPost]
        public async Task<IActionResult> Reactivate(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.isActive = true; // Mở lại tài khoản

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUser", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("UserDetail", user);
        }
        [Route("admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            try
            {
                // Gọi service đồng bộ
                var statisticsViewModel = _statisticsService.GetDashboardStatistics();
                return Ok(statisticsViewModel);
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi (log the error)
                return StatusCode(500, "Đã xảy ra lỗi máy chủ nội bộ.");
            }
        }
    }
}
