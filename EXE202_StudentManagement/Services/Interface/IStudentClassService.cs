using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IStudentClassService
    {
        IEnumerable<StudentClass> GetClassesByStudentId(string studentId);
        (bool success, string message) JoinClass(string studentId, string classCode);

    }
}
