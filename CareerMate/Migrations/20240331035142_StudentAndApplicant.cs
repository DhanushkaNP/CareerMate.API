using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class StudentAndApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Applicant",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_StudentId",
                table: "Applicant",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Student_StudentId",
                table: "Applicant",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Student_StudentId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_StudentId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Applicant");
        }
    }
}
