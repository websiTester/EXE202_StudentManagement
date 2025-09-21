namespace EXE202_StudentManagement.ViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }   // TODO / DOING / DONE
        public int? Points { get; set; }
    }
}
