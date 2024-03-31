using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorAndCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Supervisor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_CompanyId",
                table: "Supervisor",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Company_CompanyId",
                table: "Supervisor",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Company_CompanyId",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Supervisor_CompanyId",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Supervisor");
        }
    }
}
