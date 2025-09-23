using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class GradingRepository : IGradingRepository
    {
        private readonly Exe202Context _context;
        public GradingRepository (Exe202Context context)
        {
            _context = context;
        }
        public GradingViewModel GetGradingDetails(int groupId, int assignmentId)
        {
            var group = _context.Groups
                .FirstOrDefault(g => g.GroupId == groupId);

            var assignment = _context.Assignments.Find(assignmentId);

            if (group == null || assignment == null)
            {
                return null;
            }

            var submission = _context.AssignmentSubmissions.Include(a =>a.Assignment)
                .FirstOrDefault(s => s.AssignmentId == assignmentId && s.StudentId == group.LeaderId);

            var members = _context.StudentGroups
                .Where(sg => sg.GroupId == groupId)
                .Select(sg => sg.Student)
                .ToList();

            var viewModel = new GradingViewModel
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                AssignmentId = assignment.Id,
                classId = (int)submission.Assignment.ClassId,
                AssignmentName = assignment.Title,
                SubmissionLink = submission?.SubmitLink,
                GroupGrade = submission?.TeacherGrade,
                GroupComment = submission?.TeacherComment,
            };

            foreach (var member in members)
            {
                var memberGrade = _context.AssignmentGrades
                    .FirstOrDefault(ag => ag.StudentId == member.Id && ag.AssignmentId == assignmentId);

                var tasks = _context.GroupTasks
                    .Where(t => t.GroupId == groupId && t.AssignmentId == assignmentId && t.AssignedTo == member.Id)
                    .ToList();

                viewModel.Members.Add(new MemberGradingViewModel
                {
                    StudentId = member.Id,
                    FullName = $"{member.FirstName} {member.LastName}",
                    IsLeader = group.LeaderId == member.Id,
                    Grade = (decimal?)memberGrade?.Grade,
                    Comment = "", // **Lưu ý:** Bảng AssignmentGrades thiếu cột Comment
                    Tasks = tasks.Select(t => new TaskViewModel { Title = t.Title, Status = t.Status, Points = t.Points }).ToList(),
                    ToDoCount = tasks.Count(t => t.Status == "To Do"),
                    DoingCount = tasks.Count(t => t.Status == "Doing"),
                    DoneCount = tasks.Count(t => t.Status == "Done")
                });
            }

            return viewModel;
        }

        /// <summary>
        /// Lưu điểm và nhận xét vào database bằng GradingViewModel (đồng bộ).
        /// </summary>
        public void SaveGroupGrade(GradingViewModel viewModel)
        {
            // Lấy LeaderId từ GroupId
            var leaderId = _context.Groups
                .Where(g => g.GroupId == viewModel.GroupId)
                .Select(g => g.LeaderId)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(leaderId))
            {
                // Xử lý trường hợp không tìm thấy trưởng nhóm
                return;
            }

            // Tìm bài nộp của trưởng nhóm
            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(s => s.AssignmentId == viewModel.AssignmentId && s.StudentId == leaderId);

            if (submission != null)
            {
                submission.TeacherGrade = viewModel.GroupGrade;
                submission.TeacherComment = viewModel.GroupComment;
                _context.SaveChanges();
            }
        }

        // CẬP NHẬT: Thêm tham số teacherId và không dùng HttpContext
        public void SaveMemberGrades(GradingViewModel viewModel, string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
            {
                // Xử lý trường hợp không có teacherId, ví dụ: throw exception
                return;
            }

            foreach (var memberVM in viewModel.Members)
            {
                var gradeRecord = _context.AssignmentGrades
                    .FirstOrDefault(g => g.AssignmentId == viewModel.AssignmentId && g.StudentId == memberVM.StudentId);

                if (gradeRecord != null) // Cập nhật nếu đã có
                {
                    gradeRecord.Grade = (float?)memberVM.Grade;
                    // gradeRecord.Comment = memberVM.Comment;
                    gradeRecord.GradedAt = DateTime.Now;
                }
                else // Tạo mới nếu chưa có
                {
                    var newGrade = new AssignmentGrade
                    {
                        AssignmentId = viewModel.AssignmentId,
                        StudentId = memberVM.StudentId,
                        TeacherId = teacherId,
                        Grade = (float?)memberVM.Grade,
                        // Comment = memberVM.Comment,
                        GradedAt = DateTime.Now
                    };
                    _context.AssignmentGrades.Add(newGrade);
                }
            }
            _context.SaveChanges();
        }
        public AssignmentSubmission GetSubmissionLink(int assignmentId, int groupId)
        {
            var studentIdsInGroup = _context.StudentGroups
                .Where(sg => sg.GroupId == groupId)
                .Select(sg => sg.StudentId)
                .ToList();

            if (!studentIdsInGroup.Any())
            {
                return null;
            }

            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(s => s.AssignmentId == assignmentId && studentIdsInGroup.Contains(s.StudentId));

            return submission;
        }

    }
}
