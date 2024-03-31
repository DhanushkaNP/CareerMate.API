using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndInternshipPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "InternshipPost",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_CompanyId",
                table: "InternshipPost",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Company_CompanyId",
                table: "InternshipPost",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Company_CompanyId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_CompanyId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "InternshipPost");
        }
    }
}
