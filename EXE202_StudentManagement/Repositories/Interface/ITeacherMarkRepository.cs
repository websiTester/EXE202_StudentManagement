using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface ITeacherMarkRepository
    {
        Group GetGroupWithMembersByID(int groupId);
        Assignment GetAssignmentByID(int assignmentId);
        List<AssignmentSubmission> GetSubmissions(int assignmentId);
        List<GroupTask> GetGroupTasks(int groupId, int assignmentId);
        List<PeerReview> GetPeerReviews(int groupId, int assignmentId);
        string GetUserFullName(string userId);
        void AddPeerReview(PeerReview review);
        void SaveChanges();
    }
}
