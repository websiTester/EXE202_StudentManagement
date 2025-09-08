using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly Exe202Context _context;

        public StudentClassRepository(Exe202Context context)
        {
            _context = context;
        }

        public IEnumerable<StudentClass> GetClassesByStudentId(string studentId)
        {
            return _context.StudentClasses
                           .Where(sc => sc.StudentId == studentId).OrderByDescending(sc=>sc.Id)
                           .Include(sc => sc.Class)
                               .ThenInclude(c => c.Course)
                           .Include(sc => sc.Class)
                               .ThenInclude(c => c.Teacher)
                              
                           .ToList();
        }

        public bool IsStudentInClass(string studentId, int classId)
        {
            return _context.StudentClasses
                           .Any(sc => sc.StudentId == studentId && sc.ClassId == classId);
        }

        public void AddStudentToClass(StudentClass studentClass)
        {
            _context.StudentClasses.Add(studentClass);
            _context.SaveChanges();
        }
    }
}
