using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyFollowerEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFollower_Company_CompanyId1",
                table: "CompanyFollower");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFollower_Student_StudentId1",
                table: "CompanyFollower");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFollower_CompanyId1",
                table: "CompanyFollower");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFollower_StudentId1",
                table: "CompanyFollower");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "CompanyFollower");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "CompanyFollower");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "CompanyFollower",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "CompanyFollower",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_CompanyId1",
                table: "CompanyFollower",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_StudentId1",
                table: "CompanyFollower",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFollower_Company_CompanyId1",
                table: "CompanyFollower",
                column: "CompanyId1",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFollower_Student_StudentId1",
                table: "CompanyFollower",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
