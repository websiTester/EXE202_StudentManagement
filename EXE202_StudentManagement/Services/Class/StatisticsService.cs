using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public DashboardStatisticsViewModel GetDashboardStatistics()
        {
            // Lấy dữ liệu thô từ repository một cách tuần tự (đồng bộ)
            var totalUsers = _statisticsRepository.GetTotalUserCount();
            var totalStudents = _statisticsRepository.GetUserCountByRole("Student");
            var totalTeachers = _statisticsRepository.GetUserCountByRole("Teacher");
            var totalClasses = _statisticsRepository.GetTotalClassCount();
            var totalCourses = _statisticsRepository.GetTotalCourseCount();
            var totalVisits = _statisticsRepository.GetTotalVisits();
            var newUsersData = _statisticsRepository.GetNewUsersPerDay(7); // 7 ngày gần nhất
            var revenueData = _statisticsRepository.GetMonthlyRevenue(6); // 6 tháng gần nhất

            // Xử lý dữ liệu cho biểu đồ người dùng mới
            var userChartLabels = new List<string>();
            var userChartData = new List<decimal>();
            for (int i = 6; i >= 0; i--)
            {
                var date = DateTime.UtcNow.Date.AddDays(-i);
                userChartLabels.Add(date.ToString("ddd")); // Lấy tên thứ (Mon, Tue,...)
                newUsersData.TryGetValue(date, out int count);
                userChartData.Add(count);
            }

            // Đóng gói dữ liệu vào ViewModel
            var viewModel = new DashboardStatisticsViewModel
            {
                TotalUsers = totalUsers,
                TotalStudents = totalStudents,
                TotalTeachers = totalTeachers,
                TotalVisits = totalVisits,
                PercentageChange = 12.0, // Dữ liệu giả lập
                TotalClasses = totalClasses,
                TotalCourses = totalCourses,
                NewUsersChart = new ChartDataViewModel
                {
                    Labels = userChartLabels,
                    Data = userChartData
                },
                RevenueChart = new ChartDataViewModel
                {
                    Labels = revenueData.Keys.ToList(),
                    Data = revenueData.Values.ToList()
                }
            };

            return viewModel;
        }
    }
}
