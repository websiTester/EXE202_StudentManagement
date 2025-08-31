using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public int? AssignmentId { get; set; }

    public string? Title { get; set; }

    public string? FileLink { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Assignment? Assignment { get; set; }
}
