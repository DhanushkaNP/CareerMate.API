using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Skill",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CompanyId",
                table: "Skill",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Company_CompanyId",
                table: "Skill",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Company_CompanyId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_CompanyId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Skill");
        }
    }
}
