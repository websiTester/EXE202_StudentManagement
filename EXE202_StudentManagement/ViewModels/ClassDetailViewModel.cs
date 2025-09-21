using EXE202_StudentManagement.Models;
using System;
using System.Collections.Generic;

namespace EXE202_StudentManagement.ViewModels
{
    public class ClassDetailViewModel
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? TeacherName { get; set; }
        public bool IsTeacher { get; set; } // để View biết render
    }

    public class ClassDetailStudentViewModel : ClassDetailViewModel
    {
        public List<StudentAssignmentDto> Assignments { get; set; } = new();
        public GroupDto? MyGroup { get; set; }
        public List<GroupDto> AvailableGroups { get; set; } = new();
    }

    public class ClassDetailTeacherViewModel : ClassDetailViewModel
    {
        public List<TeacherAssignmentDto> Assignments { get; set; } = new();
        public List<GroupDto> Groups { get; set; } = new();
        public List<StudentDto> Students { get; set; } = new();
    }

    public class StudentAssignmentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsGroupAssignment { get; set; }
        public bool IsSubmitted { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public decimal? Grade { get; set; }
    }

    public class TeacherAssignmentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsGroupAssignment { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalStudents { get; set; }
    }

    public class GroupDto
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public List<StudentDto> Members { get; set; } = new();
        public bool IsMember { get; set; } = false;
        public int Progress { get; set; } = 0;
    }

    public class GroupSubmissionDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;

        // Thông tin bài nộp
        public int? SubmissionId { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string? SubmitLink { get; set; }
        public string? TeacherComment { get; set; }
        public decimal? TeacherGrade { get; set; }

        // Danh sách thành viên trong nhóm
        public List<StudentDto> Members { get; set; } = new();
    }

    public class StudentDto
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
    }
}
