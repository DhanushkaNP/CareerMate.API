using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class AddCvNameToStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvName",
                table: "Student",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvName",
                table: "Student");
        }
    }
}
