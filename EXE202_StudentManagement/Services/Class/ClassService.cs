using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Interface;
using EXE202_StudentManagement.ViewModels;

namespace EXE202_StudentManagement.Services.Class
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private static Random random = new Random();

        public ClassService(IClassRepository classRepository, ICourseRepository courseRepository)
        {
            _classRepository = classRepository;
            _courseRepository = courseRepository;
        }

        public (bool success, string message) CreateNewClass(CreateClassViewModel newClassModel)
        {
            int courseId = newClassModel.ExistingCourseId ?? 0;
            if (newClassModel.IsNewCourse && !string.IsNullOrEmpty(newClassModel.NewCourseName))
            {
                if (_courseRepository.IsCourseNameExist(newClassModel.NewCourseName))
                {
                    return (false, "Tên khóa học đã tồn tại.");
                }
                var newCourse = new Course
                {
                    Name = newClassModel.NewCourseName,
                    Description = newClassModel.NewCourseDescription,
                    CreateBy = newClassModel.TeacherId,
                    CreateAt = DateTime.Now
                };
                _courseRepository.AddCourse(newCourse);
                courseId = newCourse.Id;
            }

            // Tạo ClassCode ngẫu nhiên
            string classCode = GenerateRandomClassCode();
            while (_classRepository.IsClassCodeExist(classCode))
            {
                classCode = GenerateRandomClassCode();
            }

            // Tạo Class mới
            var newClass = new Models.Class
            {
                ClassName = newClassModel.ClassName,
                ClassCode = classCode,
                CreatedAt = DateTime.Now,
                CourseId = courseId,
                TeacherId = newClassModel.TeacherId
            };

            _classRepository.AddClass(newClass);

            return (true, "Tạo lớp học thành công!");
        }

        public IEnumerable<Models.Class> GetClassesByTeacherId(string teacherId)
        {
            return _classRepository.GetClassesByTeacherId(teacherId);
        }

        private string GenerateRandomClassCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
