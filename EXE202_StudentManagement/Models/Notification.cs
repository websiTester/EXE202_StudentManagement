using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? ClassCourseId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ClassCourse? ClassCourse { get; set; }
}
