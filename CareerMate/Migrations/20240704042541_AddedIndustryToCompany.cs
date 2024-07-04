using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndustryToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                table: "Company",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_IndustryId",
                table: "Company",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Industry_IndustryId",
                table: "Company",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Industry_IndustryId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_IndustryId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Company");
        }
    }
}
