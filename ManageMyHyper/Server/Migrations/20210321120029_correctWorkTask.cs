using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyHyper.Server.Migrations
{
    public partial class correctWorkTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_WorkTaskPriorities_PriorityId",
                table: "WorkTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_PriorityId",
                table: "WorkTasks");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "WorkTasks");

            migrationBuilder.RenameColumn(
                name: "WorkTaskPriority",
                table: "WorkTasks",
                newName: "WorkTaskPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_WorkTaskPriorityId",
                table: "WorkTasks",
                column: "WorkTaskPriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_WorkTaskPriorities_WorkTaskPriorityId",
                table: "WorkTasks",
                column: "WorkTaskPriorityId",
                principalTable: "WorkTaskPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_WorkTaskPriorities_WorkTaskPriorityId",
                table: "WorkTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_WorkTaskPriorityId",
                table: "WorkTasks");

            migrationBuilder.RenameColumn(
                name: "WorkTaskPriorityId",
                table: "WorkTasks",
                newName: "WorkTaskPriority");

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "WorkTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_PriorityId",
                table: "WorkTasks",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_WorkTaskPriorities_PriorityId",
                table: "WorkTasks",
                column: "PriorityId",
                principalTable: "WorkTaskPriorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
