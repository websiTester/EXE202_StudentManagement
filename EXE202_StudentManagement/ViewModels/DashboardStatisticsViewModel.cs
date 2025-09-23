namespace EXE202_StudentManagement.ViewModels
{
    public class DashboardStatisticsViewModel
    {
        // Thống kê người dùng
        public int TotalUsers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }

        // Thống kê lượt truy cập
        public int TotalVisits { get; set; }
        public double PercentageChange { get; set; } // % thay đổi so với tháng trước

        // Thống kê chung
        public int TotalClasses { get; set; }
        public int TotalCourses { get; set; }

        // Dữ liệu cho biểu đồ
        public ChartDataViewModel NewUsersChart { get; set; }
        public ChartDataViewModel RevenueChart { get; set; }
    }
}
