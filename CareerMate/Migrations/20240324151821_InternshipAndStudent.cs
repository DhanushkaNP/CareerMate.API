using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipAndStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "Student",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_InternshipId",
                table: "Student",
                column: "InternshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Internship_InternshipId",
                table: "Student",
                column: "InternshipId",
                principalTable: "Internship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Internship_InternshipId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_InternshipId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "Student");
        }
    }
}
