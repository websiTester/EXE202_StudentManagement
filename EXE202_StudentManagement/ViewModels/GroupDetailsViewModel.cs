namespace EXE202_StudentManagement.ViewModels
{
    public class GroupDetailsViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string LeaderName { get; set; }

        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }

        public List<MemberEvaluationViewModel> Members { get; set; }
    }
}
