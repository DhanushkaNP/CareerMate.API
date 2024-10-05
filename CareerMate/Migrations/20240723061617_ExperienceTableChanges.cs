using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Experience");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "Experience",
                newName: "EmploymentType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmploymentType",
                table: "Experience",
                newName: "JobType");

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedAt",
                table: "Experience",
                type: "uuid",
                nullable: true);
        }
    }
}
