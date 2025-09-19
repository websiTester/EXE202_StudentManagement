using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Services.Class
{
    public class AssignmentService : IAssignmentService
    {
        IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public void AddSubmission(AssignmentSubmission submission)
        {
            _assignmentRepository.AddSubmission(submission);
        }

        public bool DeleteSubmission(int assignmentId, string studentId)
        {
            return _assignmentRepository.DeleteSubmission(assignmentId, studentId);
        }

        public Assignment GetAssignmentById(int id)
        {
return _assignmentRepository.GetAssignmentById(id);
        }

        public AssignmentSubmission GetSubmissionByGroup(int assignmentId, int groupId)
        {
            return _assignmentRepository.GetSubmissionByGroup(assignmentId, groupId);

        }
    }
}
