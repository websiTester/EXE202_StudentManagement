using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class StudentGroup
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public string? StudentId { get; set; }

    public virtual Group? Group { get; set; }

    public virtual User? Student { get; set; }
}
