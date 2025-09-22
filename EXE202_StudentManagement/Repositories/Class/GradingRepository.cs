using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.ViewModels;

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

            var submission = _context.AssignmentSubmissions
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
        public void SaveGrading(GradingViewModel model)
        {
            // 1. Cập nhật điểm và nhận xét chung cho cả nhóm
            var group = _context.Groups.Find(model.GroupId);
            if (group == null) return;

            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(s => s.AssignmentId == model.AssignmentId && s.StudentId == group.LeaderId);

            if (submission != null)
            {
                submission.TeacherGrade = model.GroupGrade;
                submission.TeacherComment = model.GroupComment;
                _context.AssignmentSubmissions.Update(submission);
            }

            // 2. Cập nhật điểm và nhận xét cho từng thành viên
            foreach (var memberVM in model.Members)
            {
                var existingGrade = _context.AssignmentGrades
                    .FirstOrDefault(ag => ag.StudentId == memberVM.StudentId && ag.AssignmentId == model.AssignmentId);

                if (existingGrade != null)
                {
                    existingGrade.Grade = (float?)memberVM.Grade;
                    // existingGrade.Comment = memberVM.Comment; // Cần thêm cột Comment vào DB
                    existingGrade.GradedAt = DateTime.UtcNow;
                    _context.AssignmentGrades.Update(existingGrade);
                }
                else
                {
                    var newGrade = new AssignmentGrade
                    {
                        AssignmentId = model.AssignmentId,
                        StudentId = memberVM.StudentId,
                        Grade = (float?)memberVM.Grade,
                        GradedAt = DateTime.UtcNow
                        // Comment = memberVM.Comment
                    };
                    _context.AssignmentGrades.Add(newGrade);
                }
            }

            _context.SaveChanges();
        }
    }
}
