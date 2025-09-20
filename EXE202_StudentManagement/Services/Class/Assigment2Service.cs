using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Services.Class
{
    public class Assigment2Service : IAssignment2Service
    {
        private readonly IAssignment2Repository _assignmentRepository;
        public Assigment2Service(IAssignment2Repository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public List<Assignment> GetAssignmentByTeacherID(string TeacherID)
        {
            return _assignmentRepository.GetAssignmentByTeacherID(TeacherID);
        }
    }
}
