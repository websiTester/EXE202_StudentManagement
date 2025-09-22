using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IGradingRepository
    {
        GradingViewModel GetGradingDetails(int groupId, int assignmentId);
        void SaveGrading(GradingViewModel model);
    }
}
