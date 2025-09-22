namespace EXE202_StudentManagement.ViewModels
{
    public class TaskViewModel
    {
        public string Title { get; set; }
        public string Status { get; set; } // Ví dụ: "To Do", "Doing", "Done"
        public int? Points { get; set; } // Thêm thuộc tính Points
    }
}
