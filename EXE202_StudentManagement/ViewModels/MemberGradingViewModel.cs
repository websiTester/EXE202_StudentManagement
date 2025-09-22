namespace EXE202_StudentManagement.ViewModels
{
    public class MemberGradingViewModel
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public bool IsLeader { get; set; }
        public decimal? Grade { get; set; } // Điểm của cá nhân
        public string Comment { get; set; } // Nhận xét cho cá nhân

        // Thông tin công việc
        public int ToDoCount { get; set; }
        public int DoingCount { get; set; }
        public int DoneCount { get; set; }
        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
