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

            var assignment = _context.Assignments
          .Include(a => a.Class)
              .ThenInclude(c => c.Teacher) // Tải thông tin giáo viên
          .Include(a => a.Class)
              .ThenInclude(c => c.Groups) // Tải danh sách nhóm của lớp
                  .ThenInclude(g => g.StudentGroups) // Tải danh sách thành viên trong mỗi nhóm
                      .ThenInclude(sg => sg.Student) // Tải thông tin chi tiết của mỗi thành viên
          .Include(a => a.GroupTasks)
              .ThenInclude(t => t.Group) // Tải thông tin nhóm cho từng task
          .Include(a => a.Materials)
          .Include(a => a.AssignmentSubmissions)
          .FirstOrDefault(a => a.Id == id);

            return assignment;

        }
    }
}
