using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class CompanyFollowerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyFollower",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    StudentId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFollower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFollower_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyFollower_Company_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyFollower_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyFollower_Student_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_CompanyId",
                table: "CompanyFollower",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_CompanyId1",
                table: "CompanyFollower",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_StudentId",
                table: "CompanyFollower",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFollower_StudentId1",
                table: "CompanyFollower",
                column: "StudentId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyFollower");
        }
    }
}
