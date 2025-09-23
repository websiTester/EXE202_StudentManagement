using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class Assignment2Repository : IAssignment2Repository
    {
        private readonly Exe202Context _context;
        public Assignment2Repository(Exe202Context context)
        {
            _context = context;
        }
        public List<Assignment> GetAssignmentByClassID(int ClassID)
        {
            return _context.Assignments
               .Include(a => a.Class)
                   .ThenInclude(c => c.Groups)
                       .ThenInclude(g => g.StudentGroups)
               .Include(a => a.AssignmentSubmissions)
               .Where(a => a.ClassId == ClassID && a.IsGroupAssignment == true) // Lọc theo classId
               .OrderByDescending(a => a.Deadline)
               .ToList();
        }
    }
}
