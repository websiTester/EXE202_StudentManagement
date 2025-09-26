using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class GroupRepostiory : IGroupRepository
    {
        private readonly Exe202Context _context;

        public GroupRepostiory(Exe202Context context)
        {
            _context = context;
        }

        public Group GetGroupByMemberId(string memberId,int? classId)
        {
            return _context.Groups
                .Include(g => g.StudentGroups)
                    .ThenInclude(sg => sg.Student)
                .FirstOrDefault(g => g.StudentGroups.Any(sg => sg.StudentId == memberId && g.ClassId == classId));
        }
    }
}
