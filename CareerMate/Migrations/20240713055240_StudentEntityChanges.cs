using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class StudentEntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCvApproved",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CVStatus",
                table: "Student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "Student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Student",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CVStatus",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Student");

            migrationBuilder.AddColumn<bool>(
                name: "IsCvApproved",
                table: "Student",
                type: "boolean",
                nullable: true);
        }
    }
}
