using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class StudentCompanyLeaveRequestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_CompanyLeaveRequest_Id",
                table: "Student");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "CompanyLeaveRequest",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLeaveRequest_StudentId",
                table: "CompanyLeaveRequest",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyLeaveRequest_Student_StudentId",
                table: "CompanyLeaveRequest",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyLeaveRequest_Student_StudentId",
                table: "CompanyLeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_CompanyLeaveRequest_StudentId",
                table: "CompanyLeaveRequest");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CompanyLeaveRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_CompanyLeaveRequest_Id",
                table: "Student",
                column: "Id",
                principalTable: "CompanyLeaveRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
