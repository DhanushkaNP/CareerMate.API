using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipPostAndStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostedStudentId",
                table: "InternshipPost",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_PostedStudentId",
                table: "InternshipPost",
                column: "PostedStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Student_PostedStudentId",
                table: "InternshipPost",
                column: "PostedStudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Student_PostedStudentId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_PostedStudentId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "PostedStudentId",
                table: "InternshipPost");
        }
    }
}
