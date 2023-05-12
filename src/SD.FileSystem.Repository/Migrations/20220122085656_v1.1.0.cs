using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.FileSystem.Repository.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "File");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "File",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "File",
                type: "datetime2",
                nullable: true);
        }
    }
}
