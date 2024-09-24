using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MajesticAdminPanelTask.Migrations
{
    public partial class InitNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BackgroundImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BackgroundImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
