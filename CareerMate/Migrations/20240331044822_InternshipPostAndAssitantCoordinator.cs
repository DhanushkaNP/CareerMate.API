using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipPostAndAssitantCoordinator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoordinatorAssistantId",
                table: "InternshipPost",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoordinatorAssistant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<TimeSpan>(type: "interval", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinatorAssistant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPost_CoordinatorAssistantId",
                table: "InternshipPost",
                column: "CoordinatorAssistantId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_CoordinatorAssistantId",
                table: "InternshipPost",
                column: "CoordinatorAssistantId",
                principalTable: "CoordinatorAssistant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPost_CoordinatorAssistant_CoordinatorAssistantId",
                table: "InternshipPost");

            migrationBuilder.DropTable(
                name: "CoordinatorAssistant");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPost_CoordinatorAssistantId",
                table: "InternshipPost");

            migrationBuilder.DropColumn(
                name: "CoordinatorAssistantId",
                table: "InternshipPost");
        }
    }
}
