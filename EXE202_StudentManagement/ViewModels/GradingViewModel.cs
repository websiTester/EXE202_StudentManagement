using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EXE202_StudentManagement.ViewModels
{
    public class GradingViewModel
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public int classId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string SubmissionLink { get; set; } // Link file bài nộp của nhóm
        public decimal? GroupGrade { get; set; } // Điểm chung của cả nhóm
        public string GroupComment { get; set; } // Nhận xét chung của cả nhóm
        public List<MemberGradingViewModel> Members { get; set; } = new List<MemberGradingViewModel>();
    }
}
