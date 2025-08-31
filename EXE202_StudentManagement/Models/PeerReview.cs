using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class PeerReview
{
    public int ReviewId { get; set; }

    public int? GroupId { get; set; }

    public int? AssignmentId { get; set; }

    public string? ReviewerId { get; set; }

    public string? RevieweeId { get; set; }

    public string? Comment { get; set; }

    public decimal? Score { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Group? Group { get; set; }

    public virtual User? Reviewee { get; set; }

    public virtual User? Reviewer { get; set; }
}
