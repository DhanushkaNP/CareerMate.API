using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupervisorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Supervisor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Supervisor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Supervisor",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Supervisor");
        }
    }
}
