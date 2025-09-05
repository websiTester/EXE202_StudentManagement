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

        public Assignment GetAssignmentById(int id)
        {
return _assignmentRepository.GetAssignmentById(id);
        }
    }
}
