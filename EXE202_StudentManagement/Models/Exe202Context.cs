using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EXE202_StudentManagement.Models;

public partial class Exe202Context : IdentityDbContext<User>
{
    public Exe202Context()
    {
    }

    public Exe202Context(DbContextOptions<Exe202Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

    public virtual DbSet<Class> Classes { get; set; }


    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupTask> GroupTasks { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PeerReview> PeerReviews { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyCnn");
		optionsBuilder.UseSqlServer(ConnectionString);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3213E83FF33EBC35");

            entity.ToTable("Assignment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsGroupAssignment)
                .HasDefaultValue(false)
                .HasColumnName("isGroupAssignment");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Class).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Assignmen__class__68487DD7");
        });

        modelBuilder.Entity<AssignmentSubmission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Assignme__9B535595AC48F836");

            entity.ToTable("Assignment_Submission");

            entity.Property(e => e.SubmissionId).HasColumnName("submission_id");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubmitLink)
                .HasMaxLength(500)
                .HasColumnName("submit_link");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("submitted_at");
            entity.Property(e => e.TeacherComment).HasColumnName("teacher_comment");
            entity.Property(e => e.TeacherGrade)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("teacher_grade");

            entity.HasOne(d => d.Assignment).WithMany(p => p.AssignmentSubmissions)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__Assignmen__assig__76969D2E");

            entity.HasOne(d => d.Student).WithMany(p => p.AssignmentSubmissions)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Assignmen__stude__778AC167");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__FDF479862575CE96");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(255)
                .HasColumnName("class_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

			entity.HasOne(d => d.Course).WithMany(p => p.Classes)
				.HasForeignKey(d => d.CourseId)
				.HasConstraintName("FK__Class_Cou__cours__5812160E");

			entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
				.HasForeignKey(d => d.TeacherId)
				.HasConstraintName("FK__Class_Cou__teach__59063A47");
		});

        //modelBuilder.Entity<ClassCourse>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__Class_Co__3213E83FF75B44F1");

        //    entity.ToTable("Class_Course");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.ClassId).HasColumnName("class_id");
        //    entity.Property(e => e.CourseId).HasColumnName("course_id");
        //    entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

        //    entity.HasOne(d => d.Class).WithMany(p => p.ClassCourses)
        //        .HasForeignKey(d => d.ClassId)
        //        .HasConstraintName("FK__Class_Cou__class__571DF1D5");

        //    entity.HasOne(d => d.Course).WithMany(p => p.ClassCourses)
        //        .HasForeignKey(d => d.CourseId)
        //        .HasConstraintName("FK__Class_Cou__cours__5812160E");

        //    entity.HasOne(d => d.Teacher).WithMany(p => p.ClassCourses)
        //        .HasForeignKey(d => d.TeacherId)
        //        .HasConstraintName("FK__Class_Cou__teach__59063A47");
        //});

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3213E83FD9A18690");

            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy).HasColumnName("create_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK__Course__create_b__5165187F");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Group__D57795A04383A0C3");

            entity.ToTable("Group");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(255)
                .HasColumnName("group_name");

            entity.HasOne(d => d.Class).WithMany(p => p.Groups)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Group__class_cou__5FB337D6");
        });

        modelBuilder.Entity<GroupTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Group_Ta__0492148D9EC75A99");

            entity.ToTable("Group_Task");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Group_Tas__assig__72C60C4A");

            entity.HasOne(d => d.Assignment).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__Group_Tas__assig__70DDC3D8");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Group_Tas__group__71D1E811");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__6BFE1D2822950899");

            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.FileLink)
                .HasMaxLength(500)
                .HasColumnName("file_link");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("uploaded_at");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Materials)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__Materials__assig__6C190EBB");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FBC5861B6");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.ClassId).HasColumnName("class_course_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Class).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Notificat__class__02084FDA");
        });

        modelBuilder.Entity<PeerReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Peer_Rev__60883D90687E92E6");

            entity.ToTable("Peer_Review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.RevieweeId).HasColumnName("reviewee_id");
            entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");

            entity.HasOne(d => d.Assignment).WithMany(p => p.PeerReviews)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__Peer_Revi__assig__7C4F7684");

            entity.HasOne(d => d.Group).WithMany(p => p.PeerReviews)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Peer_Revi__group__7B5B524B");

            entity.HasOne(d => d.Reviewee).WithMany(p => p.PeerReviewReviewees)
                .HasForeignKey(d => d.RevieweeId)
                .HasConstraintName("FK__Peer_Revi__revie__7E37BEF6");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.PeerReviewReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .HasConstraintName("FK__Peer_Revi__revie__7D439ABD");
        });

        //modelBuilder.Entity<Role>(entity =>
        //{
        //    entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC26386540");

        //    entity.ToTable("Role");

        //    entity.Property(e => e.RoleId).HasColumnName("role_id");
        //    entity.Property(e => e.Description)
        //        .HasMaxLength(255)
        //        .HasColumnName("description");
        //});

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student___3213E83F8BBF8607");

            entity.ToTable("Student_Class");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student_C__class__5CD6CB2B");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Student_C__stude__5BE2A6F2");
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student___3213E83FC840530E");

            entity.ToTable("Student_Group");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Group).WithMany(p => p.StudentGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Student_G__group__628FA481");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentGroups)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Student_G__stude__6383C8BA");
        });

        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F6F9AACF8");

        //    entity.ToTable("User");

        //    entity.HasIndex(e => e.Email, "UQ__User__AB6E616427DBC5B4").IsUnique();

        //    entity.HasIndex(e => e.Username, "UQ__User__F3DBC572B8847A03").IsUnique();

        //    entity.Property(e => e.UserId).HasColumnName("user_id");
        //    entity.Property(e => e.Email)
        //        .HasMaxLength(255)
        //        .HasColumnName("email");
        //    entity.Property(e => e.FullName)
        //        .HasMaxLength(255)
        //        .HasColumnName("full_name");
        //    entity.Property(e => e.Password)
        //        .HasMaxLength(255)
        //        .HasColumnName("password");
        //    entity.Property(e => e.RoleId).HasColumnName("role_id");
        //    entity.Property(e => e.Username)
        //        .HasMaxLength(100)
        //        .HasColumnName("username");

        //    entity.HasOne(d => d.Role).WithMany(p => p.Users)
        //        .HasForeignKey(d => d.RoleId)
        //        .HasConstraintName("FK__User__role_id__4D94879B");
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
