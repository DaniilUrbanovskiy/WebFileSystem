using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFileSystem.DataAccess.Migrations
{
    public partial class Change_Names2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Folder");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameTable(
                name: "FileFolders",
                newName: "FileFolder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folder",
                table: "Folder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileFolder",
                table: "FileFolder",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Folder",
                table: "Folder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileFolder",
                table: "FileFolder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.RenameTable(
                name: "Folder",
                newName: "Folders");

            migrationBuilder.RenameTable(
                name: "FileFolder",
                newName: "FileFolders");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folders",
                table: "Folders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileFolders",
                table: "FileFolders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");
        }
    }
}
