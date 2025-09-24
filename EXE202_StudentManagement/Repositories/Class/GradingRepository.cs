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

            var assignment = _context.Assignments.FirstOrDefault( a => a.Id == assignmentId);

            if (group == null || assignment == null)
            {
                return null;
            }

            // Khởi tạo viewModel với các thông tin cơ bản
            var viewModel = new GradingViewModel
            {
                GroupId = groupId,
                GroupName = group.GroupName,
                AssignmentId = assignmentId,
                classId = assignment.ClassId ?? 0, // Đã thêm ClassId vào đây
                AssignmentName = assignment.Title,
                Members = new List<MemberGradingViewModel>() // Khởi tạo danh sách thành viên
            };

            var studentIdsInGroup = _context.StudentGroups
                .Where(sg => sg.GroupId == groupId)
                .Select(sg => sg.StudentId)
                .ToList();

            // Nếu không có sinh viên, trả về viewModel với thông tin cơ bản đã có
            if (!studentIdsInGroup.Any())
            {
                return viewModel;
            }

            // Tìm kiếm bài nộp. Bước này là tùy chọn và sẽ không gây lỗi nếu không tìm thấy.
            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(s => s.AssignmentId == assignmentId && studentIdsInGroup.Contains(s.StudentId));

            // Nếu có bài nộp, cập nhật viewModel với thông tin từ bài nộp
            if (submission != null)
            {
                viewModel.SubmissionLink = submission.SubmitLink;
                viewModel.GroupGrade = submission.TeacherGrade;
                viewModel.GroupComment = submission.TeacherComment;
            }

            // Tiếp tục lấy thông tin các thành viên và công việc như bình thường
            var membersInfo = _context.Users
                .Where(u => studentIdsInGroup.Contains(u.Id))
                .ToList();

            var allGradesForGroup = _context.AssignmentGrades
                .Where(ag => ag.AssignmentId == assignmentId && studentIdsInGroup.Contains(ag.StudentId))
                .ToList();

            var allTasksForGroup = _context.GroupTasks
                .Where(t => t.GroupId == groupId && t.AssignmentId == assignmentId)
                .ToList();

            foreach (var member in membersInfo)
            {
                var memberGrade = allGradesForGroup.FirstOrDefault(ag => ag.StudentId == member.Id);
                var memberTasks = allTasksForGroup.Where(t => t.AssignedTo == member.Id).ToList();

                viewModel.Members.Add(new MemberGradingViewModel
                {
                    StudentId = member.Id,
                    FullName = $"{member.FirstName} {member.LastName}",
                    IsLeader = group.LeaderId == member.Id,
                    Grade = (decimal?)(memberGrade?.Grade),
                    Comment = memberGrade?.Comment,
                    Tasks = memberTasks.Select(t => new TaskViewModel
                    {
                        Title = t.Title,
                        Status = t.Status,
                        Points = t.Points ?? 0
                    }).ToList()
                });
            }

            return viewModel;
        }

        /// <summary>
        /// Lưu điểm và nhận xét vào database bằng GradingViewModel (đồng bộ).
        /// </summary>
        public void SaveGroupGrade(GradingViewModel viewModel)
        {
            var studentIdsInGroup = _context.StudentGroups
            .Where(sg => sg.GroupId == viewModel.GroupId)
            .Select(sg => sg.StudentId)
            .ToList();

            if (!studentIdsInGroup.Any())
            {
                return; // Không có thành viên để lưu điểm
            }

            // Tìm bài nộp của bất kỳ thành viên nào trong nhóm
            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(s => s.AssignmentId == viewModel.AssignmentId && studentIdsInGroup.Contains(s.StudentId));

            // Nếu có bài nộp, cập nhật nó
            if (submission != null)
            {
                submission.TeacherGrade = viewModel.GroupGrade;
                submission.TeacherComment = viewModel.GroupComment;
            }
            
            _context.SaveChanges();
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
                    gradeRecord.Comment = memberVM.Comment;
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
