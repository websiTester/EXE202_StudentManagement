using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }
	public string? ClassCode { get; set; }

    public DateTime? CreatedAt { get; set; }
	public int? CourseId { get; set; }

	public string? TeacherId { get; set; }

	public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

	//public virtual ICollection<ClassCourse> ClassCourses { get; set; } = new List<ClassCourse>();

	public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
	public virtual Course? Course { get; set; }

	public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

	public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

	public virtual User? Teacher { get; set; }
}
