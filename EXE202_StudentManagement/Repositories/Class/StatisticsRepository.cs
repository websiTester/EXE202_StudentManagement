using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly Exe202Context _context;
        public StatisticsRepository(Exe202Context context)
        {
            _context = context;
        }
        public int GetTotalUserCount()
        {
            return _context.Users.Count();
        }

        public int GetUserCountByRole(string roleName)
        {
            // Truy vấn để đếm người dùng dựa trên vai trò (role)
            return (from user in _context.Users
                    join userRole in _context.UserRoles on user.Id equals userRole.UserId
                    join role in _context.Roles on userRole.RoleId equals role.Id
                    where role.Name == roleName
                    select user).Count();
        }

        public int GetTotalClassCount()
        {
            return _context.Classes.Count();
        }

        public int GetTotalCourseCount()
        {
            return _context.Courses.Count();
        }

        public Dictionary<DateTime, int> GetNewUsersPerDay(int days)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-days + 1);
            return _context.Users
                .Where(u => u.CreateAt.HasValue && u.CreateAt.Value.Date >= startDate)
                .GroupBy(u => u.CreateAt.Value.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .AsEnumerable() // Chuyển sang xử lý ở client để dùng ToDictionary
                .ToDictionary(x => x.Date, x => x.Count);
        }

        // --- PHẦN GIẢ LẬP ---
        public int GetTotalVisits()
        {
            // Dữ liệu giả lập
            return 15830;
        }

        public Dictionary<string, decimal> GetMonthlyRevenue(int months)
        {
            var revenueData = new Dictionary<string, decimal>();
            var culture = new System.Globalization.CultureInfo("vi-VN");
            for (int i = months - 1; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                var monthName = $"Tháng {date.Month}";
                // Dữ liệu doanh thu giả lập (đơn vị: triệu VND)
                decimal value = 50 + (i * 10) + new Random().Next(-5, 5);
                revenueData.Add(monthName, value);
            }
            return revenueData;
        }
    }
}
