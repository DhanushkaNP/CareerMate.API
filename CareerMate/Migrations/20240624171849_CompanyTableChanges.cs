using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStudents",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Company",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfStudents",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
