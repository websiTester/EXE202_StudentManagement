using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Assignment
{
    public int Id { get; set; }

    public int? ClassId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsGroupAssignment { get; set; }

    public virtual ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();

    public virtual Class? Class { get; set; }

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<PeerReview> PeerReviews { get; set; } = new List<PeerReview>();
}
