namespace EXE202_StudentManagement.ViewModels
{
    public class IntroductionViewModel
    {
        public bool IsAuthenticated { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
        public string? UserName { get; set; }
    }
}
