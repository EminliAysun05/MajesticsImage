using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MajesticAdminPanelTask.Migrations
{
    public partial class InitNewElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BackgroundImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BackgroundImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BackgroundImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BackgroundImages");
        }
    }
}
