using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IAssignment2Service
    {
        List<Assignment> GetAssignmentByTeacherID(string TeacherID);
    }
}
