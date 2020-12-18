using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EpicUniversity.Migrations
{
    public partial class AddedCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLab_Courses_CourseId",
                table: "CourseLab");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLab",
                table: "CourseLab");

            migrationBuilder.RenameTable(
                name: "CourseLab",
                newName: "CourseLabs");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLab_CourseId",
                table: "CourseLabs",
                newName: "IX_CourseLabs_CourseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Personnel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CourseLabs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLabs",
                table: "CourseLabs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLabs_Courses_CourseId",
                table: "CourseLabs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLabs_Courses_CourseId",
                table: "CourseLabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLabs",
                table: "CourseLabs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CourseLabs");

            migrationBuilder.RenameTable(
                name: "CourseLabs",
                newName: "CourseLab");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLabs_CourseId",
                table: "CourseLab",
                newName: "IX_CourseLab_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLab",
                table: "CourseLab",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLab_Courses_CourseId",
                table: "CourseLab",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
