using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class InternshipInvitesRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternshipInvite");

            migrationBuilder.CreateTable(
                name: "InternshipOffer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipOffer_Internship_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipOffer_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipOffer_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternshipOffer_InternshipId",
                table: "InternshipOffer",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipOffer_StudentId",
                table: "InternshipOffer",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipOffer_SupervisorId",
                table: "InternshipOffer",
                column: "SupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternshipOffer");

            migrationBuilder.CreateTable(
                name: "InternshipInvite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipInvite_Internship_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipInvite_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternshipInvite_InternshipId",
                table: "InternshipInvite",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipInvite_StudentId",
                table: "InternshipInvite",
                column: "StudentId");
        }
    }
}
