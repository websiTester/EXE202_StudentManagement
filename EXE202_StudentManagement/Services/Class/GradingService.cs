using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class GradingService :IGradingService
    {
        private readonly IGradingRepository _repository;
        public GradingService(IGradingRepository repository)
        {
            _repository = repository;
        }

        public GradingViewModel GetGradingDetails(int groupId, int assignmentId)
        {
            return _repository.GetGradingDetails(groupId, assignmentId);
        }

        public AssignmentSubmission GetSubmissionLink(int assignmentId, int groupId)
        {
            return _repository.GetSubmissionLink(assignmentId, groupId);
        }

        public void SaveGroupGrade(GradingViewModel viewModel)
        {
             _repository.SaveGroupGrade(viewModel);
        }

        public void SaveMemberGrades(GradingViewModel viewModel, string teacherId)
        {
            _repository.SaveMemberGrades(viewModel, teacherId);
        }
    }
}
