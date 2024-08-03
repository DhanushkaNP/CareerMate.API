using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class DailyDiaryTableRefactorWithIntern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary");

            migrationBuilder.AddColumn<Guid>(
                name: "InternId",
                table: "DailyDiary",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiary_InternId",
                table: "DailyDiary",
                column: "InternId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDiary_Intern_InternId",
                table: "DailyDiary",
                column: "InternId",
                principalTable: "Intern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDiary_Intern_InternId",
                table: "DailyDiary");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary");

            migrationBuilder.DropIndex(
                name: "IX_DailyDiary_InternId",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "InternId",
                table: "DailyDiary");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
