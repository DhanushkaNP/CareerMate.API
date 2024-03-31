using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInternshipPostInternshipStudentRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Internship_InternshipId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "InternshipPostStudent");

            migrationBuilder.DropIndex(
                name: "IX_Student_InternshipId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "Student",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InternshipPostStudent",
                columns: table => new
                {
                    InternshipPostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipPostStudent", x => new { x.InternshipPostsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InternshipPostStudent_InternshipPost_InternshipPostsId",
                        column: x => x.InternshipPostsId,
                        principalTable: "InternshipPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipPostStudent_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_InternshipId",
                table: "Student",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPostStudent_StudentsId",
                table: "InternshipPostStudent",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Internship_InternshipId",
                table: "Student",
                column: "InternshipId",
                principalTable: "Internship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
