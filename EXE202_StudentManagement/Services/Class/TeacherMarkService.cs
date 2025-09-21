using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class TeacherMarkService : ITeacherMarkService
    {
        private readonly ITeacherMarkRepository _repository;
        public TeacherMarkService(ITeacherMarkRepository repository)
        {
            _repository = repository;
        }

        public GroupDetailsViewModel GetGroupDetails(int groupId, int assignmentId)
        {
            var group = _repository.GetGroupWithMembersByID(groupId);
            var assignment = _repository.GetAssignmentByID(assignmentId);
            var submissions = _repository.GetSubmissions(assignmentId);
            var tasks = _repository.GetGroupTasks(groupId, assignmentId);
            var peerReviews = _repository.GetPeerReviews(groupId, assignmentId);

            var model = new GroupDetailsViewModel
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                LeaderName = group.LeaderId != null
                    ?  _repository.GetUserFullName(group.LeaderId)
                    : null,
                AssignmentId = assignmentId,
                AssignmentTitle = assignment?.Title,
                Members = group.StudentGroups.Select(sg =>
                {
                    var sub = submissions.FirstOrDefault(s => s.StudentId == sg.StudentId);
                    var memberTasks = tasks.Where(t => t.AssignedTo == sg.StudentId).ToList();
                    var reviews = peerReviews.Where(r => r.RevieweeId == sg.StudentId).ToList();

                    return new MemberEvaluationViewModel
                    {
                        StudentId = sg.StudentId,
                        FullName = sg.Student.FirstName + " " + sg.Student.LastName,
                        SubmitLink = sub?.SubmitLink,
                        SubmittedAt = sub?.SubmittedAt,
                        TeacherComment = sub?.TeacherComment,
                        TeacherGrade = sub?.TeacherGrade,
                        Tasks = memberTasks.Select(t => new TaskViewModel
                        {
                            TaskId = t.TaskId,
                            Title = t.Title,
                            Status = t.Status,
                            Points = t.Points
                        }).ToList(),
                        PeerReviews = reviews.Select(r => new PeerReviewViewModel
                        {
                            ReviewerId = r.ReviewerId,
                            //ReviewerName = _repository.GetUserFullName(r.ReviewerId),
                            //Score = r.Score,
                            Comment = r.Comment
                        }).ToList()
                    };
                }).ToList()
            };

            return model;
        }

        public void SaveEvaluation(GroupDetailsViewModel model, string reviewerId)
        {
            foreach (var member in model.Members)
            {
                if (member.Score.HasValue || !string.IsNullOrEmpty(member.Comment))
                {
                    var review = new PeerReview
                    {
                        GroupId = model.GroupId,
                        AssignmentId = model.AssignmentId,
                        ReviewerId = reviewerId,
                        RevieweeId = member.StudentId,
                        Comment = member.Comment,
                        Score = (decimal?)member.Score
                    };
                    _repository.AddPeerReview(review);
                }
            }

            _repository.SaveChanges();
        }
    }
}
