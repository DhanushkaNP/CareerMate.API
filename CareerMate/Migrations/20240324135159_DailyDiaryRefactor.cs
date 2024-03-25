using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class DailyDiaryRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinatorApproved",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "SupervisorApproved",
                table: "DailyDiary");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InternshipPeriodTo",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InternshipPeriodFrom",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

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
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedCoordinatorApprovalAt",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedSupervisorApprovalAt",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoordinatorApproved",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "IsSupervisorApproved",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "RequestedCoordinatorApprovalAt",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "RequestedSupervisorApprovalAt",
                table: "DailyDiary");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InternshipPeriodTo",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InternshipPeriodFrom",
                table: "DailyDiary",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "CoordinatorApproved",
                table: "DailyDiary",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SupervisorApproved",
                table: "DailyDiary",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
