using Microsoft.EntityFrameworkCore.Migrations;

namespace EpicUniversity.Migrations
{
    public partial class ChangeSudentGradeToGpa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Gpa",
                table: "Personnel",
                type: "decimal(2,1)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "Grades",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "Personnel",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpa",
                table: "Grades",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");
        }
    }
}
