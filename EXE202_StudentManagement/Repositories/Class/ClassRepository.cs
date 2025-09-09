using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class ClassRepository : IClassRepository
    {


        private readonly Exe202Context _context;

        public ClassRepository(Exe202Context context)
        {
            _context = context;
        }

        public void AddClass(Models.Class newClass)
        {
            _context.Classes.Add(newClass);
            _context.SaveChanges();
        }

        public Models.Class GetClassByCode(string classCode)
        {
            return _context.Classes.FirstOrDefault(c => c.ClassCode == classCode);

        }

        public IEnumerable<Models.Class> GetClassesByTeacherId(string teacherId)
        {
            return _context.Classes
            .Where(c => c.TeacherId == teacherId).OrderByDescending(c=>c.CreatedAt)
            .Include(c => c.Course)
            .ToList();
        }

        public bool IsClassCodeExist(string classCode)
        {
            return _context.Classes.Any(c => c.ClassCode == classCode);
        }
    }
}
