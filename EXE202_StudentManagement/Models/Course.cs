using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<ClassCourse> ClassCourses { get; set; } = new List<ClassCourse>();

    public virtual User? CreateByNavigation { get; set; }
}
