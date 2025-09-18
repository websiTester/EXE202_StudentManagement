using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Group_Task",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Group_Task");
        }
    }
}
