using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailForFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Faculty",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Faculty");
        }
    }
}
