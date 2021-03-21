using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyHyper.Server.Migrations
{
    public partial class WorkTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateHasBeenDone = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PriorityId = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkTaskPriority = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTasks_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkTasks_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkTasks_WorkTaskPriorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "WorkTaskPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_AssignedUserId",
                table: "WorkTasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_CreatorUserId",
                table: "WorkTasks",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_PriorityId",
                table: "WorkTasks",
                column: "PriorityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkTasks");
        }
    }
}
