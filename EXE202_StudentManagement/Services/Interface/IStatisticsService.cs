using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IStatisticsService
    {
        DashboardStatisticsViewModel GetDashboardStatistics();
    }
}
