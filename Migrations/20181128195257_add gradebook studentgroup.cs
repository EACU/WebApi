using Microsoft.EntityFrameworkCore.Migrations;

namespace EACAAPI.Migrations
{
    public partial class addgradebookstudentgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gradebook",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Gradebook",
                table: "StudentGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gradebook",
                table: "StudentGroups");

            migrationBuilder.AddColumn<string>(
                name: "Gradebook",
                table: "Students",
                nullable: false,
                defaultValue: "");
        }
    }
}
