using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyHyper.Server.Migrations
{
    public partial class updateWorkTaskAddNullableAssignedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedUserId",
                table: "WorkTasks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedUserId",
                table: "WorkTasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_AssignedUserId",
                table: "WorkTasks",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
