using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFileSystem.DataAccess.Migrations
{
    public partial class Change_File_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "Folder");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "File");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "Folder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
