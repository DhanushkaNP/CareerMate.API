using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorAndIntern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorId",
                table: "Intern",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intern_SupervisorId",
                table: "Intern",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intern_Supervisor_SupervisorId",
                table: "Intern",
                column: "SupervisorId",
                principalTable: "Supervisor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intern_Supervisor_SupervisorId",
                table: "Intern");

            migrationBuilder.DropIndex(
                name: "IX_Intern_SupervisorId",
                table: "Intern");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Intern");
        }
    }
}
