using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IClassRepository2
    {
        Task<Models.Class?> GetClassDetailAsync(int classId);
        Task AddAssignmentAsync(Assignment assignment);
        Task AddGroupsAsync(List<Group> groups);
        Task<int?> GetClassIdByGroupIdAsync(int groupId);
        Task AddStudentToGroupAsync(int groupId, string studentId);
        Task<bool> ExistsStudentInGroupAsync(int groupId, string studentId);
        Task<List<User>> GetStudentsInClassAsync(int classId);
        Task RemoveStudentFromGroupAsync(int groupId, string studentId);

        // service cần
        Task<List<AssignmentSubmission>> GetSubmissionsByAssignmentAsync(int assignmentId);
        Task<Group?> GetStudentGroupAsync(int classId, string studentId);

        Task SaveChangesAsync();
    }
}
