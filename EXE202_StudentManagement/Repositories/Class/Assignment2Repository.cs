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
        public List<Assignment> GetAssignmentByTeacherID(string TeacherID)
        {
            return _context.Assignments.Include(a => a.AssignmentSubmissions)
                .Include(a => a.GroupTasks).Where(a =>a.Class.TeacherId == TeacherID).ToList();
        }
    }
}
