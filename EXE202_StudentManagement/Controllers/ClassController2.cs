using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("class")]
public class ClassDetailController : Controller
{
    private readonly IClassService2 _service;
    private readonly UserManager<User> _userManager;

    public ClassDetailController(IClassService2 service, UserManager<User> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        var currentUserId = _userManager.GetUserId(User);
        var vm = await _service.GetClassDetailViewModelAsync(id, currentUserId);

        if (vm == null) return NotFound();

        if (vm.IsTeacher)
        {
            return View("~/Views/Class/DetailTeacher.cshtml", vm as ClassDetailTeacherViewModel);
        }
        else
        {
            return View("~/Views/Class/DetailStudent.cshtml", vm as ClassDetailStudentViewModel);
        }
    }

    [HttpPost("add-assignment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAssignment(Assignment assignment)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAssignmentAsync(assignment);
            TempData["SuccessMessage"] = "Thêm bài tập thành công!";
            return RedirectToAction("Detail", new { id = assignment.ClassId });
        }

        TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
        return RedirectToAction("Detail", new { id = assignment.ClassId });
    }

    [HttpPost("add-groups")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGroups(int classId, int numberOfGroups, bool randomize = false, bool createEmptyGroups = false)
    {
        var groups = new List<Group>();
        for (int i = 1; i <= numberOfGroups; i++)
        {
            groups.Add(new Group { ClassId = classId, GroupName = $"Nhóm {i}" });
        }
        await _service.AddGroupsAsync(groups);

        if (randomize)
        {
            await _service.RandomizeStudentsIntoGroups(classId, groups);
        }

        TempData["SuccessMessage"] = "Tạo nhóm thành công!";
        return RedirectToAction("Detail", new { id = classId });
    }

    [HttpPost("join-group")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> JoinGroup(int groupId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _service.AddStudentToGroupAsync(groupId, userId);
        TempData["SuccessMessage"] = "Bạn đã tham gia nhóm!";
        var classId = await _service.GetClassIdByGroupIdAsync(groupId);
        return RedirectToAction("Detail", new { id = classId });
    }
}
