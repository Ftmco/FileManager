using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace File.DataBase.Migrations
{
    public partial class UpdateDire2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Directory",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Directory");
        }
    }
}
