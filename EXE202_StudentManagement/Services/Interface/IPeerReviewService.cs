using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Services.Interface
{
    public interface IPeerReviewService
    {
        bool AddPeerReview(PeerReview review);
        List<PeerReview> GetReviewsForMember(string revieweeId, int assignmentId);
        bool HasReviewed(string reviewerId, string revieweeId, int assignmentId);
        PeerReview GetExistingReview(string reviewerId, string revieweeId, int assignmentId);
        bool UpdatePeerReview(PeerReview review);

    }
}
