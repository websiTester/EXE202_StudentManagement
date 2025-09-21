using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface ITeacherMarkService
    {
        GroupDetailsViewModel GetGroupDetails(int groupId, int assignmentId);
        void SaveEvaluation(GroupDetailsViewModel model, string reviewerId);
    }
}
