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

            return _context.Assignments
                               .Include(a => a.Class)
                                   .ThenInclude(c => c.Teacher) // Lấy thông tin giáo viên
                               .Include(a => a.GroupTasks)
                                   .ThenInclude(t => t.AssignedToNavigation) // Bao gồm thông tin người được giao
                               .Include(a => a.GroupTasks)
                                   .ThenInclude(t => t.Group) // Bao gồm thông tin nhóm
                               .Include(a => a.Materials)
                               .Include(a => a.AssignmentSubmissions)
                               .FirstOrDefault(a => a.Id == id);

        }
    }
}
