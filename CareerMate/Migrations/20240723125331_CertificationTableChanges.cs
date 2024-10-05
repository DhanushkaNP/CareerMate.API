using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CertificationTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Certification");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Certification",
                newName: "IssuedMonth");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Certification",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Certification");

            migrationBuilder.RenameColumn(
                name: "IssuedMonth",
                table: "Certification",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteAt",
                table: "Certification",
                type: "uuid",
                nullable: true);
        }
    }
}
