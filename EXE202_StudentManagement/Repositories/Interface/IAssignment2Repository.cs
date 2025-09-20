using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IAssignment2Repository
    {
        List<Assignment> GetAssignmentByTeacherID(string TeacherID);
    }
}
