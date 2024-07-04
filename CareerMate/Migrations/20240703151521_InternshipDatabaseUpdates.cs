using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipDatabaseUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_CoordinatorAssistantId",
                table: "InternshipPost");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Coordinator_CoordinatorId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "Flyer",
                table: "InternshipPost");

            migrationBuilder.RenameColumn(
                name: "CoordinatorId",
                table: "InternshipPost",
                newName: "PostedCoordinatorId");

            migrationBuilder.RenameColumn(
                name: "CoordinatorAssistantId",
                table: "InternshipPost",
                newName: "PostedCoordinatorAssistantId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "InternshipPost",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPost_CoordinatorId",
                table: "InternshipPost",
                newName: "IX_InternshipPost_PostedCoordinatorId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPost_CoordinatorAssistantId",
                table: "InternshipPost",
                newName: "IX_InternshipPost_PostedCoordinatorAssistantId");

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "InternshipPost",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FlyerUrl",
                table: "InternshipPost",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Internship",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_FacultyId",
                table: "InternshipPost",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Internship_FacultyId",
                table: "Internship",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internship_Faculty_FacultyId",
                table: "Internship",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_PostedCoordinatorAssist~",
                table: "InternshipPost",
                column: "PostedCoordinatorAssistantId",
                principalTable: "CoordinatorAssistant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Coordinator_PostedCoordinatorId",
                table: "InternshipPost",
                column: "PostedCoordinatorId",
                principalTable: "Coordinator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Faculty_FacultyId",
                table: "InternshipPost",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internship_Faculty_FacultyId",
                table: "Internship");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_PostedCoordinatorAssist~",
                table: "InternshipPost");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Coordinator_PostedCoordinatorId",
                table: "InternshipPost");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Faculty_FacultyId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_FacultyId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_Internship_FacultyId",
                table: "Internship");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "FlyerUrl",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Internship");

            migrationBuilder.RenameColumn(
                name: "PostedCoordinatorId",
                table: "InternshipPost",
                newName: "CoordinatorId");

            migrationBuilder.RenameColumn(
                name: "PostedCoordinatorAssistantId",
                table: "InternshipPost",
                newName: "CoordinatorAssistantId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "InternshipPost",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPost_PostedCoordinatorId",
                table: "InternshipPost",
                newName: "IX_InternshipPost_CoordinatorId");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipPost_PostedCoordinatorAssistantId",
                table: "InternshipPost",
                newName: "IX_InternshipPost_CoordinatorAssistantId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Flyer",
                table: "InternshipPost",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_CoordinatorAssistantId",
                table: "InternshipPost",
                column: "CoordinatorAssistantId",
                principalTable: "CoordinatorAssistant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Coordinator_CoordinatorId",
                table: "InternshipPost",
                column: "CoordinatorId",
                principalTable: "Coordinator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
