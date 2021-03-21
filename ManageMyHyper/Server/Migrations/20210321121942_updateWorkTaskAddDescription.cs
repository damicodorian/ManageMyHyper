using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageMyHyper.Server.Migrations
{
    public partial class updateWorkTaskAddDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkTasks",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkTasks");
        }
    }
}
