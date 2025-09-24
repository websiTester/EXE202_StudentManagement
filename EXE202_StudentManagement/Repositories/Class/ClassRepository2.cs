using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class ClassRepository2 : IClassRepository2
    {
        private readonly Exe202Context _context;

        public ClassRepository2(Exe202Context context)
        {
            _context = context;
        }

        public async Task<Models.Class?> GetClassDetailAsync(int classId)
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Assignments)
                    .ThenInclude(a => a.AssignmentSubmissions)
                .Include(c => c.Groups)
                    .ThenInclude(g => g.StudentGroups)
                        .ThenInclude(sg => sg.Student)
                .Include(c => c.StudentClasses)
                    .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }

        public async Task AddAssignmentAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
        }

        public async Task AddGroupsAsync(List<Group> groups)
        {
            await _context.Groups.AddRangeAsync(groups);
        }

        public async Task<int?> GetClassIdByGroupIdAsync(int groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId);
            return group?.ClassId;
        }

        public async Task AddStudentToGroupAsync(int groupId, string studentId)
        {
            var exists = await _context.StudentGroups
                .AnyAsync(sg => sg.GroupId == groupId && sg.StudentId == studentId);

            if (!exists)
            {
                _context.StudentGroups.Add(new StudentGroup
                {
                    GroupId = groupId,
                    StudentId = studentId
                });
            }
        }

        public async Task<bool> ExistsStudentInGroupAsync(int groupId, string studentId)
        {
            return await _context.StudentGroups
                .AnyAsync(sg => sg.GroupId == groupId && sg.StudentId == studentId);
        }

        public async Task<List<User>> GetStudentsInClassAsync(int classId)
        {
            return await _context.StudentClasses
                .Where(sc => sc.ClassId == classId)
                .Select(sc => sc.Student!)
                .ToListAsync();
        }

        public async Task RemoveStudentFromGroupAsync(int groupId, string studentId)
        {
            var studentGroup = await _context.StudentGroups
                .FirstOrDefaultAsync(sg => sg.GroupId == groupId && sg.StudentId == studentId);

            if (studentGroup != null)
            {
                _context.StudentGroups.Remove(studentGroup);
            }
        }

        // Lấy toàn bộ submissions của 1 Assignment
        public async Task<List<AssignmentSubmission>> GetSubmissionsByAssignmentAsync(int assignmentId)
        {
            return await _context.AssignmentSubmissions
                .Where(s => s.AssignmentId == assignmentId)
                .Include(s => s.Student)
                .ToListAsync();
        }

        // Lấy group của 1 student trong class
        public async Task<Group?> GetStudentGroupAsync(int classId, string studentId)
        {
            return await _context.Groups
                .Include(g => g.StudentGroups)
                    .ThenInclude(sg => sg.Student)
                .FirstOrDefaultAsync(g =>
                    g.ClassId == classId &&
                    g.StudentGroups.Any(sg => sg.StudentId == studentId));
        }

        public async Task<bool> IsUserTeacherInAnyClassAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;
            return await _context.Classes.AnyAsync(c => c.TeacherId == userId);
        }

        public async Task<bool> IsUserStudentInAnyClassAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;
            return await _context.StudentClasses.AnyAsync(sc => sc.StudentId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
