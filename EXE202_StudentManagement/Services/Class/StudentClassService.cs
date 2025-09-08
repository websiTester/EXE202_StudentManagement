using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Services.Class
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly IClassRepository _classRepository;

        public StudentClassService(IStudentClassRepository studentClassRepository, IClassRepository classRepository)
        {
            _studentClassRepository = studentClassRepository;
            _classRepository = classRepository;
        }

        public IEnumerable<StudentClass> GetClassesByStudentId(string studentId)
        {
            return _studentClassRepository.GetClassesByStudentId(studentId);
        }

        public (bool success, string message) JoinClass(string studentId, string classCode)
        {
            // 1. Tìm lớp học theo mã
            var classToJoin = _classRepository.GetClassByCode(classCode);
            if (classToJoin == null)
            {
                return (false, "Mã lớp học không hợp lệ.");
            }

            // 2. Kiểm tra xem sinh viên đã tham gia lớp chưa
            if (_studentClassRepository.IsStudentInClass(studentId, classToJoin.ClassId))
            {
                return (false, "Bạn đã tham gia lớp học này rồi.");
            }

            // 3. Thêm sinh viên vào lớp
            var newStudentClass = new StudentClass
            {
                StudentId = studentId,
                ClassId = classToJoin.ClassId
            };
            _studentClassRepository.AddStudentToClass(newStudentClass);

            return (true, "Tham gia lớp học thành công!");
        }
    }
}
