using Microsoft.EntityFrameworkCore.Migrations;

namespace ofsen.Migrations
{
    public partial class yarat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thumb",
                table: "Pano");

            migrationBuilder.AddColumn<string>(
                name: "thumbName",
                table: "Pano",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thumbName",
                table: "Pano");

            migrationBuilder.AddColumn<string>(
                name: "thumb",
                table: "Pano",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
