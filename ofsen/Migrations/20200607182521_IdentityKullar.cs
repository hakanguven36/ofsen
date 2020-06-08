using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ofsen.Migrations
{
    public partial class IdentityKullar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullar");

            migrationBuilder.AddColumn<bool>(
                name: "silindi",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "uyelikTarihi",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "silindi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "uyelikTarihi",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Kullar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    beniHatirla = table.Column<bool>(type: "bit", nullable: false),
                    cerez = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    cerezEnc = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    eposta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    epostaCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    epostaCodeEnc = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    epostaEnc = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    epostaOnay = table.Column<bool>(type: "bit", nullable: false),
                    hatali = table.Column<int>(type: "int", nullable: false),
                    kilitli = table.Column<bool>(type: "bit", nullable: false),
                    password = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    passwordEnc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    silindi = table.Column<bool>(type: "bit", nullable: false),
                    uyelikTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullar", x => x.ID);
                });
        }
    }
}
