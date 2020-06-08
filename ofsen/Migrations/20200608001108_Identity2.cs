using Microsoft.EntityFrameworkCore.Migrations;

namespace ofsen.Migrations
{
    public partial class Identity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kullanıcıAdı",
                table: "AspNetUsers",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HesaplarGirişYapModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(nullable: false),
                    password = table.Column<string>(maxLength: 12, nullable: false),
                    hatırla = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HesaplarGirişYapModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HesaplarÜyeOlModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(maxLength: 12, nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(maxLength: 12, nullable: false),
                    confirmpassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HesaplarÜyeOlModel", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HesaplarGirişYapModel");

            migrationBuilder.DropTable(
                name: "HesaplarÜyeOlModel");

            migrationBuilder.DropColumn(
                name: "kullanıcıAdı",
                table: "AspNetUsers");
        }
    }
}
