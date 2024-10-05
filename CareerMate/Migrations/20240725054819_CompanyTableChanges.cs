using System;
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
            migrationBuilder.AddColumn<DateTime>(
                name: "FoundedOn",
                table: "Company",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalRaters",
                table: "Company",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRatings",
                table: "Company",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundedOn",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "TotalRaters",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "TotalRatings",
                table: "Company");
        }
    }
}
