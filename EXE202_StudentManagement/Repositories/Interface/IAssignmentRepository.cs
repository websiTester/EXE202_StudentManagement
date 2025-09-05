using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IAssignmentRepository
    {
        Assignment GetAssignmentById(int id);
    }
}
