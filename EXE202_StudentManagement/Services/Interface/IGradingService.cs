using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IGradingService
    {
        GradingViewModel GetGradingDetails(int groupId, int assignmentId);
        bool SaveGrading(GradingViewModel model);
    }
}
