using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IAssignmentRepository
    {
        Assignment GetAssignmentById(int id);

        void AddSubmission(AssignmentSubmission submission);

        AssignmentSubmission GetSubmissionByGroup(int assignmentId, int groupId);
        bool DeleteSubmission(int assignmentId, string studentId);



    }


}
