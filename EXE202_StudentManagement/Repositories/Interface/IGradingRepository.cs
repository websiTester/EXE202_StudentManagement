using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IGradingRepository
    {
        GradingViewModel GetGradingDetails(int groupId, int assignmentId);
        void SaveGroupGrade(GradingViewModel viewModel);
        void SaveMemberGrades(GradingViewModel viewModel, string teacherId);
        AssignmentSubmission GetSubmissionLink(int assignmentId, int groupId);
    }
}
