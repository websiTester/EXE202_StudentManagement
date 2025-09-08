using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class ClassRepository : IClassRepository
    {


        private readonly Exe202Context _context;

        public ClassRepository(Exe202Context context)
        {
            _context = context;
        }

        public Models.Class GetClassByCode(string classCode)
        {
            return _context.Classes.FirstOrDefault(c => c.ClassCode == classCode);

        }
    }
}
