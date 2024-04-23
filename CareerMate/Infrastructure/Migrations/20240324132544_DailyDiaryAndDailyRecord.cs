using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerMate.Migrations
{
    /// <inheritdoc />
    public partial class DailyDiaryAndDailyRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyDiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodCoveredFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodCoveredTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InternshipPeriodFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InternshipPeriodTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TrainingLocation = table.Column<string>(type: "text", nullable: true),
                    SupervisorApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CoordinatorApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDiary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DiaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRecord_DailyDiary_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "DailyDiary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyRecord_DiaryId",
                table: "DailyRecord",
                column: "DiaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyRecord");

            migrationBuilder.DropTable(
                name: "DailyDiary");
        }
    }
}
