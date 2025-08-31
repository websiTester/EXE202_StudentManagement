using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    class_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class__FDF479862575CE96", x => x.class_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_by = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__3213E83FD9A18690", x => x.id);
                    table.ForeignKey(
                        name: "FK__Course__create_b__5165187F",
                        column: x => x.create_by,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student_Class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    class_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student___3213E83F8BBF8607", x => x.id);
                    table.ForeignKey(
                        name: "FK__Student_C__class__5CD6CB2B",
                        column: x => x.class_id,
                        principalTable: "Class",
                        principalColumn: "class_id");
                    table.ForeignKey(
                        name: "FK__Student_C__stude__5BE2A6F2",
                        column: x => x.student_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Class_Course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_id = table.Column<int>(type: "int", nullable: true),
                    course_id = table.Column<int>(type: "int", nullable: true),
                    teacher_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class_Co__3213E83FF75B44F1", x => x.id);
                    table.ForeignKey(
                        name: "FK__Class_Cou__class__571DF1D5",
                        column: x => x.class_id,
                        principalTable: "Class",
                        principalColumn: "class_id");
                    table.ForeignKey(
                        name: "FK__Class_Cou__cours__5812160E",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Class_Cou__teach__59063A47",
                        column: x => x.teacher_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_course_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    isGroupAssignment = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assignme__3213E83FF33EBC35", x => x.id);
                    table.ForeignKey(
                        name: "FK__Assignmen__class__68487DD7",
                        column: x => x.class_course_id,
                        principalTable: "Class_Course",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_course_id = table.Column<int>(type: "int", nullable: true),
                    group_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Group__D57795A04383A0C3", x => x.group_id);
                    table.ForeignKey(
                        name: "FK__Group__class_cou__5FB337D6",
                        column: x => x.class_course_id,
                        principalTable: "Class_Course",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    class_course_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__E059842FBC5861B6", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK__Notificat__class__02084FDA",
                        column: x => x.class_course_id,
                        principalTable: "Class_Course",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment_Submission",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignment_id = table.Column<int>(type: "int", nullable: true),
                    student_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    submit_link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    submitted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    teacher_comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    teacher_grade = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assignme__9B535595AC48F836", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK__Assignmen__assig__76969D2E",
                        column: x => x.assignment_id,
                        principalTable: "Assignment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Assignmen__stude__778AC167",
                        column: x => x.student_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    material_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignment_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    file_link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__6BFE1D2822950899", x => x.material_id);
                    table.ForeignKey(
                        name: "FK__Materials__assig__6C190EBB",
                        column: x => x.assignment_id,
                        principalTable: "Assignment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Group_Task",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assignment_id = table.Column<int>(type: "int", nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    assigned_to = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Group_Ta__0492148D9EC75A99", x => x.task_id);
                    table.ForeignKey(
                        name: "FK__Group_Tas__assig__70DDC3D8",
                        column: x => x.assignment_id,
                        principalTable: "Assignment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Group_Tas__assig__72C60C4A",
                        column: x => x.assigned_to,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Group_Tas__group__71D1E811",
                        column: x => x.group_id,
                        principalTable: "Group",
                        principalColumn: "group_id");
                });

            migrationBuilder.CreateTable(
                name: "Peer_Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    assignment_id = table.Column<int>(type: "int", nullable: true),
                    reviewer_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reviewee_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Peer_Rev__60883D90687E92E6", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Peer_Revi__assig__7C4F7684",
                        column: x => x.assignment_id,
                        principalTable: "Assignment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Peer_Revi__group__7B5B524B",
                        column: x => x.group_id,
                        principalTable: "Group",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK__Peer_Revi__revie__7D439ABD",
                        column: x => x.reviewer_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Peer_Revi__revie__7E37BEF6",
                        column: x => x.reviewee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student_Group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    student_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student___3213E83FC840530E", x => x.id);
                    table.ForeignKey(
                        name: "FK__Student_G__group__628FA481",
                        column: x => x.group_id,
                        principalTable: "Group",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK__Student_G__stude__6383C8BA",
                        column: x => x.student_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_class_course_id",
                table: "Assignment",
                column: "class_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Submission_assignment_id",
                table: "Assignment_Submission",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Submission_student_id",
                table: "Assignment_Submission",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Class_Course_class_id",
                table: "Class_Course",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Class_Course_course_id",
                table: "Class_Course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Class_Course_teacher_id",
                table: "Class_Course",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Course_create_by",
                table: "Course",
                column: "create_by");

            migrationBuilder.CreateIndex(
                name: "IX_Group_class_course_id",
                table: "Group",
                column: "class_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Task_assigned_to",
                table: "Group_Task",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Task_assignment_id",
                table: "Group_Task",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_Task_group_id",
                table: "Group_Task",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_assignment_id",
                table: "Materials",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_class_course_id",
                table: "Notification",
                column: "class_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Peer_Review_assignment_id",
                table: "Peer_Review",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Peer_Review_group_id",
                table: "Peer_Review",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Peer_Review_reviewee_id",
                table: "Peer_Review",
                column: "reviewee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Peer_Review_reviewer_id",
                table: "Peer_Review",
                column: "reviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Class_class_id",
                table: "Student_Class",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Class_student_id",
                table: "Student_Class",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_group_id",
                table: "Student_Group",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Group_student_id",
                table: "Student_Group",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Assignment_Submission");

            migrationBuilder.DropTable(
                name: "Group_Task");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Peer_Review");

            migrationBuilder.DropTable(
                name: "Student_Class");

            migrationBuilder.DropTable(
                name: "Student_Group");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Class_Course");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
