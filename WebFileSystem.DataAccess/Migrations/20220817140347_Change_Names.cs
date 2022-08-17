using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFileSystem.DataAccess.Migrations
{
    public partial class Change_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUrl",
                table: "UserUrl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urls",
                table: "Urls");

            migrationBuilder.RenameTable(
                name: "UserUrl",
                newName: "FileFolders");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "Urls",
                newName: "Folders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileFolders",
                table: "FileFolders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folders",
                table: "Folders",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Folders",
                table: "Folders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileFolders",
                table: "FileFolders");

            migrationBuilder.RenameTable(
                name: "Folders",
                newName: "Urls");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "FileFolders",
                newName: "UserUrl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urls",
                table: "Urls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUrl",
                table: "UserUrl",
                column: "Id");
        }
    }
}
