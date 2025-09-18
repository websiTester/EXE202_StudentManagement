using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly Exe202Context _context;

        public AssignmentRepository(Exe202Context context)
        {
            _context = context;
        }
        public Assignment GetAssignmentById(int id)
        {

            var assignment = _context.Assignments
          .Include(a => a.Class)
              .ThenInclude(c => c.Teacher) // Tải thông tin giáo viên
          .Include(a => a.Class)
              .ThenInclude(c => c.Groups) // Tải danh sách nhóm của lớp
                  .ThenInclude(g => g.StudentGroups) // Tải danh sách thành viên trong mỗi nhóm
                      .ThenInclude(sg => sg.Student) // Tải thông tin chi tiết của mỗi thành viên
          .Include(a => a.GroupTasks)
              .ThenInclude(t => t.Group) // Tải thông tin nhóm cho từng task
          .Include(a => a.Materials)
          .Include(a => a.AssignmentSubmissions)
          .FirstOrDefault(a => a.Id == id);

            return assignment;

        }
        public void AddSubmission(AssignmentSubmission submission)
        {
            try
            {
                _context.AssignmentSubmissions.Add(submission);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public AssignmentSubmission GetSubmissionByGroup(int assignmentId, int groupId)
        {
            var studentIdsInGroup = _context.StudentGroups
                                            .Where(sg => sg.GroupId == groupId)
                                            .Select(sg => sg.StudentId)
                                            .ToList();

            return _context.AssignmentSubmissions
                           .FirstOrDefault(s => s.AssignmentId == assignmentId && studentIdsInGroup.Contains(s.StudentId));
        }

        public bool DeleteSubmission(int assignmentId, string studentId)
        {
            var submission = _context.AssignmentSubmissions
                    .FirstOrDefault(s => s.AssignmentId == assignmentId && s.StudentId == studentId);

            if (submission == null)
            {
                return false;
            }

            // Xóa file vật lý trên server nếu là bài nộp bằng file
            if (!submission.SubmitLink.StartsWith("http"))
            {
                var filePath = Path.Combine("wwwroot", submission.SubmitLink.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.AssignmentSubmissions.Remove(submission);
            _context.SaveChanges();
            return true;
        }
    }
}
