using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class StudentAndDegree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DegreeId",
                table: "Student",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Degree",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_DegreeId",
                table: "Student",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Degree_FacultyId",
                table: "Degree",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Degree_Faculty_FacultyId",
                table: "Degree",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Degree_DegreeId",
                table: "Student",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degree_Faculty_FacultyId",
                table: "Degree");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Degree_DegreeId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_DegreeId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Degree_FacultyId",
                table: "Degree");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Degree");
        }
    }
}
