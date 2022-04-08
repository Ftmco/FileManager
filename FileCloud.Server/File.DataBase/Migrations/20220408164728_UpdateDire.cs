using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace File.DataBase.Migrations
{
    public partial class UpdateDire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FDirectoryId",
                table: "Directory",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Directory",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directory_FDirectoryId",
                table: "Directory",
                column: "FDirectoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directory_Directory_FDirectoryId",
                table: "Directory",
                column: "FDirectoryId",
                principalTable: "Directory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directory_Directory_FDirectoryId",
                table: "Directory");

            migrationBuilder.DropIndex(
                name: "IX_Directory_FDirectoryId",
                table: "Directory");

            migrationBuilder.DropColumn(
                name: "FDirectoryId",
                table: "Directory");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Directory");
        }
    }
}
