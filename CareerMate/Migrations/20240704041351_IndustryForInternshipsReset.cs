using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class IndustryForInternshipsReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internship_Industry_IndustryId",
                table: "Internship");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Industry_IndustryId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_IndustryId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_Internship_IndustryId",
                table: "Internship");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Internship");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPositions",
                table: "InternshipPost",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPositions",
                table: "InternshipPost");

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                table: "InternshipPost",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                table: "Internship",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_IndustryId",
                table: "InternshipPost",
                column: "IndustryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Internship_IndustryId",
                table: "Internship",
                column: "IndustryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Internship_Industry_IndustryId",
                table: "Internship",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Industry_IndustryId",
                table: "InternshipPost",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
