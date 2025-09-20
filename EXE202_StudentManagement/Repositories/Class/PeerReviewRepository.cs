using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class PeerReviewRepository : IPeerReviewRepository
    {
        private readonly Exe202Context _context;

        public PeerReviewRepository(Exe202Context context)
        {
            _context = context;
        }



        public bool Add(PeerReview review)
        {
            try
            {
                _context.PeerReviews.Add(review);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PeerReview GetExistingReview(string reviewerId, string revieweeId, int assignmentId)
        {
            return _context.PeerReviews
                               .FirstOrDefault(pr => pr.ReviewerId == reviewerId
                                                  && pr.RevieweeId == revieweeId
                                                  && pr.AssignmentId == assignmentId);
        }

        public List<PeerReview> GetReviewsForMember(string revieweeId, int assignmentId)
        {
            return _context.PeerReviews
                     .Where(pr => pr.RevieweeId == revieweeId && pr.AssignmentId == assignmentId)
                     .ToList();
        }

        public bool HasReviewed(string reviewerId, string revieweeId, int assignmentId)
        {
            return _context.PeerReviews
                                   .Any(pr => pr.ReviewerId == reviewerId && pr.RevieweeId == revieweeId && pr.AssignmentId == assignmentId);
        }

        public bool Update(PeerReview review)
        {
            try
            {
                _context.PeerReviews.Update(review);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
