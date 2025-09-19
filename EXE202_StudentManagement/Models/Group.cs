using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public int? ClassId { get; set; }

    public string? GroupName { get; set; }
    public string? LeaderId { get; set; }

	public virtual Class? Class { get; set; }
    public virtual User? Leader { get; set; }

    public virtual ICollection<GroupTask> GroupTasks { get; set; } = new List<GroupTask>();

    public virtual ICollection<PeerReview> PeerReviews { get; set; } = new List<PeerReview>();

    public virtual ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
}
