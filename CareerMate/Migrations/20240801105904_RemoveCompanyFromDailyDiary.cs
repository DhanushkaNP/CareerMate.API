using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyFromDailyDiary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDiary_Company_CompanyId",
                table: "DailyDiary");

            migrationBuilder.DropIndex(
                name: "IX_DailyDiary_CompanyId",
                table: "DailyDiary");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DailyDiary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "DailyDiary",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiary_CompanyId",
                table: "DailyDiary",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDiary_Company_CompanyId",
                table: "DailyDiary",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
