using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Repositories.Class
{
    public class CourseRepository : ICourseRepository
    {
        private readonly Exe202Context _context;

        public CourseRepository(Exe202Context context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetCoursesByTeacherId(string teacherId)
        {
            return _context.Courses.Where(c => c.CreateBy == teacherId).ToList();
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public bool IsCourseNameExist(string name)
        {
            return _context.Courses.Any(c => c.Name == name);
        }
    }
}
