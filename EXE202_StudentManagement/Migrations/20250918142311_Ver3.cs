using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AspNetUsers");
        }
    }
}
