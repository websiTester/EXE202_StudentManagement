namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IStatisticsRepository
    {
        int GetTotalUserCount();
        int GetUserCountByRole(string roleName);
        int GetTotalClassCount();
        int GetTotalCourseCount();
        Dictionary<DateTime, int> GetNewUsersPerDay(int days);

        // --- Các phương thức giả lập ---
        int GetTotalVisits(); // Cần bảng Log để triển khai thực tế
        Dictionary<string, decimal> GetMonthlyRevenue(int months); // Cần bảng Payment để triển khai thực tế
    }
}
