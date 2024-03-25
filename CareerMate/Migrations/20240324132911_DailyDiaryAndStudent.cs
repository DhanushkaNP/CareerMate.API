using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class DailyDiaryAndStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "DailyDiary",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiary_StudentId",
                table: "DailyDiary",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDiary_Student_StudentId",
                table: "DailyDiary");

            migrationBuilder.DropIndex(
                name: "IX_DailyDiary_StudentId",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "DailyDiary");
        }
    }
}
