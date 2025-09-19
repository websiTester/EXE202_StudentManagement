using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IAssignmentService
    {
        Assignment GetAssignmentById(int id);
        void AddSubmission(AssignmentSubmission submission);
        AssignmentSubmission GetSubmissionByGroup(int assignmentId, int groupId);
        bool DeleteSubmission(int assignmentId, string studentId);

    }
}
