using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? ClassId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Class? Class { get; set; }
}
