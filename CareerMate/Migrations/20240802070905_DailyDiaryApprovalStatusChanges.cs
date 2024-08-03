using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class DailyDiaryApprovalStatusChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoordinatorApproved",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "IsSupervisorApproved",
                table: "DailyDiary");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorApprovalStatus",
                table: "DailyDiary",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorApprovalStatus",
                table: "DailyDiary",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinatorApprovalStatus",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "SupervisorApprovalStatus",
                table: "DailyDiary");

            migrationBuilder.AddColumn<bool>(
                name: "IsCoordinatorApproved",
                table: "DailyDiary",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisorApproved",
                table: "DailyDiary",
                type: "boolean",
                nullable: true,
                defaultValue: false);
        }
    }
}
