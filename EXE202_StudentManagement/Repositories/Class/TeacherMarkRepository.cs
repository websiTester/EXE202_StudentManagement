using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class TeacherMarkRepository : ITeacherMarkRepository
    {
        private readonly Exe202Context _context;
        public TeacherMarkRepository(Exe202Context context)
        {
            _context = context;
        }

        public void AddPeerReview(PeerReview review)
        {
            _context.PeerReviews.Add(review);
        }

        public Assignment GetAssignmentByID(int assignmentId)
        {
            return _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);
        }

        public List<GroupTask> GetGroupTasks(int groupId, int assignmentId)
        {
            return _context.GroupTasks.Where(gt => gt.GroupId == groupId &&  gt.AssignmentId == assignmentId).ToList();
        }

        public Group GetGroupWithMembersByID(int groupId)
        {
            return _context.Groups.Include(g => g.StudentGroups).ThenInclude(sg => sg.Student).FirstOrDefault(g => g.GroupId == groupId);
        }

        public List<PeerReview> GetPeerReviews(int groupId, int assignmentId)
        {
            return _context.PeerReviews.Where(p => p.GroupId == groupId &&  p.AssignmentId == assignmentId).ToList();
        }

        public List<AssignmentSubmission> GetSubmissions(int assignmentId)
        {
            return _context.AssignmentSubmissions.Where(s => s.AssignmentId == assignmentId).ToList();
        }

        public string GetUserFullName(string userId)
        {
            return _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.FirstName + " " + u.LastName)
                .FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
