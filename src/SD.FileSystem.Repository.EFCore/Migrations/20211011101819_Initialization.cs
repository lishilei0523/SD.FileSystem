using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.FileSystem.Repository.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "CHAR(16) CHARACTER SET OCTETS", nullable: false),
                    ExtensionName = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    Size = table.Column<long>(type: "BIGINT", nullable: false),
                    HashValue = table.Column<string>(type: "VARCHAR(32)", maxLength: 32, nullable: false),
                    RelativePath = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    AbsolutePath = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    HostName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    Url = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Use = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    Description = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Number = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Keywords = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    SavedTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Deleted = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    CreatorAccount = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    CreatorName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    OperatorAccount = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true),
                    OperatorName = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashValue",
                table: "File",
                column: "HashValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");
        }
    }
}
