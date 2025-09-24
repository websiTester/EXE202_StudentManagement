using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class ClassService2 : IClassService2
    {
        private readonly IClassRepository2 _repo;

        public ClassService2(IClassRepository2 repo)
        {
            _repo = repo;
        }

        public async Task<ClassDetailViewModel?> GetClassDetailViewModelAsync(int classId, string? currentUserId = null)
        {
            var classEntity = await _repo.GetClassDetailAsync(classId);
            if (classEntity == null) return null;

            bool isTeacher = classEntity.TeacherId == currentUserId;

            if (isTeacher)
            {
                var vm = new ClassDetailTeacherViewModel
                {
                    ClassId = classEntity.ClassId,
                    ClassName = classEntity.ClassName,
                    TeacherName = classEntity.Teacher != null
                        ? $"{classEntity.Teacher.FirstName} {classEntity.Teacher.LastName}"
                        : "Chưa có giáo viên",
                    IsTeacher = true,
                    Assignments = classEntity.Assignments.Select(a => new TeacherAssignmentDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Deadline = a.Deadline,
                        IsGroupAssignment = a.IsGroupAssignment ?? false,
                        TotalSubmissions = a.AssignmentSubmissions.Count,
                        TotalStudents = classEntity.StudentClasses.Count,
                        Groups = classEntity.Groups.Select(g => new GroupSubmissionStatusDto
                        {
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            HasSubmitted = a.AssignmentSubmissions
                                .Any(s => g.StudentGroups.Any(sg => sg.StudentId == s.StudentId))
                        }).ToList()
                    }).ToList(),
                    Groups = classEntity.Groups.Select(g =>
                    {
                        var groupAssignments = classEntity.Assignments
                            .Where(a => a.IsGroupAssignment == true)
                            .ToList();

                        int totalGroupAssignments = groupAssignments.Count;

                        int submittedCount = groupAssignments.Count(a =>
                            a.AssignmentSubmissions.Any(s =>
                                g.StudentGroups.Any(sg => sg.StudentId == s.StudentId))
                        );

                        int progressPercent = totalGroupAssignments == 0 ? 0 :
                            (int)Math.Round((double)submittedCount / totalGroupAssignments * 100);

                        return new GroupDto
                        {
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            Members = g.StudentGroups.Select(sg => new StudentDto
                            {
                                UserId = sg.StudentId,
                                FullName = sg.Student != null
                                    ? $"{sg.Student.FirstName} {sg.Student.LastName}"
                                    : "Unknown"
                            }).ToList(),
                            Progress = progressPercent
                        };
                    }).ToList(),
                    Students = classEntity.StudentClasses.Select(sc => new StudentDto
                    {
                        UserId = sc.StudentId,
                        FullName = sc.Student != null
                            ? $"{sc.Student.FirstName} {sc.Student.LastName}"
                            : "Unknown"
                    }).ToList()
                };
                return vm;
            }
            else
            {
                var vm = new ClassDetailStudentViewModel
                {
                    ClassId = classEntity.ClassId,
                    ClassName = classEntity.ClassName,
                    TeacherName = classEntity.Teacher != null
                        ? $"{classEntity.Teacher.FirstName} {classEntity.Teacher.LastName}"
                        : "Chưa có giáo viên",
                    IsTeacher = false,
                    Assignments = classEntity.Assignments.Select(a =>
                    {
                        var submission = a.AssignmentSubmissions
                            .FirstOrDefault(s => s.StudentId == currentUserId);

                        return new StudentAssignmentDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Deadline = a.Deadline,
                            IsGroupAssignment = a.IsGroupAssignment ?? false,
                            IsSubmitted = submission != null,
                            SubmittedAt = submission?.SubmittedAt,
                            Grade = submission?.TeacherGrade
                        };
                    }).ToList(),
                    MyGroup = classEntity.Groups
                        .Select(g => new GroupDto
                        {
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            Members = g.StudentGroups.Select(sg => new StudentDto
                            {
                                UserId = sg.StudentId,
                                FullName = sg.Student != null
                                    ? $"{sg.Student.FirstName} {sg.Student.LastName}"
                                    : "Unknown"
                            }).ToList(),
                            IsMember = g.StudentGroups.Any(sg => sg.StudentId == currentUserId)
                        })
                        .FirstOrDefault(g => g.IsMember),
                    AvailableGroups = classEntity.Groups.Select(g => new GroupDto
                    {
                        GroupId = g.GroupId,
                        GroupName = g.GroupName,
                        Members = g.StudentGroups.Select(sg => new StudentDto
                        {
                            UserId = sg.StudentId,
                            FullName = sg.Student != null
                                ? $"{sg.Student.FirstName} {sg.Student.LastName}"
                                : "Unknown"
                        }).ToList(),
                        IsMember = g.StudentGroups.Any(sg => sg.StudentId == currentUserId)
                    }).ToList()
                };
                return vm;
            }
        }



        public async Task AddAssignmentAsync(Assignment assignment)
        {
            assignment.Id = 0;
            assignment.CreatedAt = DateTime.Now;
            await _repo.AddAssignmentAsync(assignment);
            await _repo.SaveChangesAsync();
        }

        public async Task AddGroupsAsync(List<Group> groups)
        {
            await _repo.AddGroupsAsync(groups);
            await _repo.SaveChangesAsync();
        }

        public async Task RandomizeStudentsIntoGroups(int classId, List<Group> groups)
        {
            var students = await _repo.GetStudentsInClassAsync(classId);
            var rnd = new Random();
            var shuffled = students.OrderBy(x => rnd.Next()).ToList();

            int i = 0;
            foreach (var student in shuffled)
            {
                var group = groups[i % groups.Count];
                await _repo.AddStudentToGroupAsync(group.GroupId, student.Id);
                i++;
            }
            await _repo.SaveChangesAsync();
        }

        public async Task AddStudentToGroupAsync(int groupId, string studentId)
        {
            await _repo.AddStudentToGroupAsync(groupId, studentId);
            await _repo.SaveChangesAsync();
        }

        public async Task RemoveStudentFromGroupAsync(int groupId, string studentId)
        {
            await _repo.RemoveStudentFromGroupAsync(groupId, studentId);
            await _repo.SaveChangesAsync();
        }

        public Task<int?> GetClassIdByGroupIdAsync(int groupId)
        {
            return _repo.GetClassIdByGroupIdAsync(groupId);
        }
        public async Task<List<Models.Class>> GetClassesForUserAsync(string userId)
        {
            var isTeacher = await _repo.IsUserTeacherInAnyClassAsync(userId);

            if (isTeacher)
            {
                return await _repo.GetClassesByTeacherAsync(userId);
            }

            return await _repo.GetClassesByStudentAsync(userId);
        }

    }
}
