using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class StudentClass
{
    public int Id { get; set; }

    public string? StudentId { get; set; }

    public int? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual User? Student { get; set; }
}
