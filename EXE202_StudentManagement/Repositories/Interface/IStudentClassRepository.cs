using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IStudentClassRepository
    {
        IEnumerable<StudentClass> GetClassesByStudentId(string studentId);
        bool IsStudentInClass(string studentId, int classId);
        void AddStudentToClass(StudentClass studentClass);

    }
}
