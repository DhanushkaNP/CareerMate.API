using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipInviteAndInternship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "InternshipInvite",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InternshipInvite_InternshipId",
                table: "InternshipInvite",
                column: "InternshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipInvite_Internship_InternshipId",
                table: "InternshipInvite",
                column: "InternshipId",
                principalTable: "Internship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipInvite_Internship_InternshipId",
                table: "InternshipInvite");

            migrationBuilder.DropIndex(
                name: "IX_InternshipInvite_InternshipId",
                table: "InternshipInvite");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "InternshipInvite");
        }
    }
}
