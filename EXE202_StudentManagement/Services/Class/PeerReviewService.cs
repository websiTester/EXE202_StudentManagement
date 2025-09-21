using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Services.Class
{
    public class PeerReviewService : IPeerReviewService
    {
        private readonly IPeerReviewRepository _reviewRepository;

        public PeerReviewService(IPeerReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }


        public bool AddPeerReview(PeerReview review)
        {
            return _reviewRepository.Add(review);
        }

        public PeerReview GetExistingReview(string reviewerId, string revieweeId, int assignmentId)
        {
            return _reviewRepository.GetExistingReview(reviewerId, revieweeId, assignmentId);
        }

        public List<PeerReview> GetReviewsForMember(string revieweeId, int assignmentId)
        {
            return _reviewRepository.GetReviewsForMember(revieweeId, assignmentId);
        }

        public bool HasReviewed(string reviewerId, string revieweeId, int assignmentId)
        {
            return _reviewRepository.HasReviewed(reviewerId, revieweeId, assignmentId);
        }

        public bool UpdatePeerReview(PeerReview review)
        {
            return _reviewRepository.Update(review);
        }
    }
}
