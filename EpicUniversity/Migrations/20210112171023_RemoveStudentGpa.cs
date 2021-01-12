using Microsoft.EntityFrameworkCore.Migrations;

namespace EpicUniversity.Migrations
{
    public partial class RemoveStudentGpa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gpa",
                table: "Students",
                type: "decimal(5,2)",
                nullable: true);
        }
    }
}
