using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Ver4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaderId",
                table: "Group",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_LeaderId",
                table: "Group",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_AspNetUsers_LeaderId",
                table: "Group",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_AspNetUsers_LeaderId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_LeaderId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Group");
        }
    }
}
