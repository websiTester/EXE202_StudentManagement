namespace EXE202_StudentManagement.ViewModels
{
    public class AssigmentViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DueDate { get; set; }
        public int SubmittedCount { get; set; }
        public int TotalCount { get; set; }
        public List<GroupViewModel> Groups { get; set; } = new List<GroupViewModel>();
    }
}
