using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternEntityUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Intern",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndedDate",
                table: "Intern",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedDate",
                table: "Intern",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "EndedDate",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "StartedDate",
                table: "Intern");
        }
    }
}
