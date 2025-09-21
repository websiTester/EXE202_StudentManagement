namespace EXE202_StudentManagement.ViewModels
{
    public class MemberEvaluationViewModel
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }

        // Nộp bài
        public string SubmitLink { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string TeacherComment { get; set; }
        public decimal? TeacherGrade { get; set; }

        // Task trong nhóm
        public List<TaskViewModel> Tasks { get; set; }

        // Peer review
        public List<PeerReviewViewModel> PeerReviews { get; set; }

        // Để nhập form chấm điểm
        public double? Score { get; set; }
        public string Comment { get; set; }
    }
}
