using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternAndInternship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "Intern",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IsDeletedAt",
                table: "Intern",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intern_InternshipId",
                table: "Intern",
                column: "InternshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intern_Internship_InternshipId",
                table: "Intern",
                column: "InternshipId",
                principalTable: "Internship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intern_Internship_InternshipId",
                table: "Intern");

            migrationBuilder.DropIndex(
                name: "IX_Intern_InternshipId",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "IsDeletedAt",
                table: "Intern");
        }
    }
}
