using EXE202_StudentManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_StudentManagement.Repositories.Interface
{
    public interface ICourseRepository 
    {
        IEnumerable<Course> GetCoursesByTeacherId(string teacherId);
        void AddCourse(Course course);
        bool IsCourseNameExist(string name);
    }
}
