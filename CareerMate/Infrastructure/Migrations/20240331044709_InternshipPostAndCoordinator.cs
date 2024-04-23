using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipPostAndCoordinator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoordinatorId",
                table: "InternshipPost",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_CoordinatorId",
                table: "InternshipPost",
                column: "CoordinatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_Coordinator_CoordinatorId",
                table: "InternshipPost",
                column: "CoordinatorId",
                principalTable: "Coordinator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_Coordinator_CoordinatorId",
                table: "InternshipPost");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_CoordinatorId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "InternshipPost");
        }
    }
}
