using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IClassService
    {
        IEnumerable<Models.Class> GetClassesByTeacherId(string teacherId);
        (bool success, string message) CreateNewClass(CreateClassViewModel newClassModel);
    }
}
