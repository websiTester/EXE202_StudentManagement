using EXE202_StudentManagement.Models;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface IPeerReviewRepository
    {
        bool Add(PeerReview review);
        List<PeerReview> GetReviewsForMember(string revieweeId, int assignmentId);
        bool HasReviewed(string reviewerId, string revieweeId, int assignmentId);

        PeerReview GetExistingReview(string reviewerId, string revieweeId, int assignmentId);
        bool Update(PeerReview review);

    }
}
