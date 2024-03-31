using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndInterns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Company_CompanyId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CompanyId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Student");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Intern",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intern_CompanyId",
                table: "Intern",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intern_Company_CompanyId",
                table: "Intern",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intern_Company_CompanyId",
                table: "Intern");

            migrationBuilder.DropIndex(
                name: "IX_Intern_CompanyId",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Intern");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Student",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_CompanyId",
                table: "Student",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Company_CompanyId",
                table: "Student",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
