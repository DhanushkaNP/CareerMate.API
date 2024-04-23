using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternsipPostAndApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedApplicationUserId",
                table: "InternshipPost",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserRole",
                table: "InternshipPost",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "InternshipPost",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_CreatedApplicationUserId",
                table: "InternshipPost",
                column: "CreatedApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_AspNetUsers_CreatedApplicationUserId",
                table: "InternshipPost",
                column: "CreatedApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_AspNetUsers_CreatedApplicationUserId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_CreatedApplicationUserId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "CreatedApplicationUserId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "CreatedUserRole",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "InternshipPost");
        }
    }
}
