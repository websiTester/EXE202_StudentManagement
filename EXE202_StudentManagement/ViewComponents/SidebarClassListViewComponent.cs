using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.ViewComponents
{
    public class SidebarClassListViewComponent : ViewComponent
    {
        private readonly IClassService2 _classService;
        private readonly UserManager<User> _userManager;

        public SidebarClassListViewComponent(IClassService2 classService, UserManager<User> userManager)
        {
            _classService = classService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return View(new List<Class>());

            var classes = await _classService.GetClassesForUserAsync(user.Id);
            return View(classes);
        }
    }
}
