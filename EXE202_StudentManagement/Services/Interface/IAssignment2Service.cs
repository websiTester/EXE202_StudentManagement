using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IAssignment2Service
    {
        List<AssigmentViewModel> GetAllClassAssignment(int classID);
    }
}
