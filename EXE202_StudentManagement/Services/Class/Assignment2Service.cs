using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class Assignment2Service : IAssignment2Service
    {
        private readonly IAssignment2Repository assignment2Repository;
        public Assignment2Service(IAssignment2Repository assignment2)
        {
            assignment2Repository = assignment2;
        }
        public List<AssigmentViewModel> GetAllClassAssignment(int classID)
        {
            var assignments = assignment2Repository.GetAssignmentByClassID(classID);
            return assignments.Select(assignment =>
            {
                var studentsWhoSubmitted = assignment.AssignmentSubmissions
                    .Select(s => s.StudentId)
                    .ToHashSet();

                var groupViewModels = assignment.Class.Groups.Select(group =>
                {
                    // SỬA LỖI: Kiểm tra null cho StudentGroups trước khi truy cập
                    var studentsInGroup = group.StudentGroups?
                        .Select(sg => sg.StudentId)
                        .ToHashSet() ?? new HashSet<string?>();

                    bool isSubmitted = studentsInGroup.Overlaps(studentsWhoSubmitted);

                    return new GroupViewModel
                    {
                        Id = group.GroupId,
                        Name = group.GroupName,
                        Submitted = isSubmitted
                    };
                }).ToList();

                return new AssigmentViewModel
                {
                    Id = assignment.Id,
                    Title = assignment.Title,
                    DueDate = assignment.Deadline?.ToString("dd/MM/yyyy"),
                    Groups = groupViewModels,
                    TotalCount = groupViewModels.Count,
                    SubmittedCount = groupViewModels.Count(g => g.Submitted)
                };
            }).ToList();
        }
    }
}
