using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IClassService2
    {
        Task<ClassDetailViewModel?> GetClassDetailViewModelAsync(int classId, string? currentUserId = null);

        Task AddAssignmentAsync(Assignment assignment);
        Task AddGroupsAsync(List<Group> groups);
        Task RandomizeStudentsIntoGroups(int classId, List<Group> groups);
        Task AddStudentToGroupAsync(int groupId, string studentId);
        Task RemoveStudentFromGroupAsync(int groupId, string studentId);
        Task<int?> GetClassIdByGroupIdAsync(int groupId);
        Task<List<Models.Class>> GetClassesForUserAsync(string userId);
    }
}
