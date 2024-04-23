using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class StudentAndSkillRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Skill");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Skill",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skill",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Skill",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "From",
                table: "Skill",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "Skill",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "To",
                table: "Skill",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
