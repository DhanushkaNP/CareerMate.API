using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Link",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Link_CompanyId",
                table: "Link",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Company_CompanyId",
                table: "Link",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Link_Company_CompanyId",
                table: "Link");

            migrationBuilder.DropIndex(
                name: "IX_Link_CompanyId",
                table: "Link");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Link");
        }
    }
}
