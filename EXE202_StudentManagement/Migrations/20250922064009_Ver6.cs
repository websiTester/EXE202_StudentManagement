using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Ver6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "AssignmentGrades",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "AssignmentGrades");
        }
    }
}
