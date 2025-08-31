using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class ClassCourse
{
    public int Id { get; set; }

    public int? ClassId { get; set; }

    public int? CourseId { get; set; }

    public string? TeacherId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Class? Class { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User? Teacher { get; set; }
}
