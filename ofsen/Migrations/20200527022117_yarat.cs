using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ofsen.Migrations
{
    public partial class yarat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eposta = table.Column<string>(maxLength: 60, nullable: false),
                    mesaj = table.Column<string>(maxLength: 600, nullable: false),
                    ipno = table.Column<string>(maxLength: 20, nullable: true),
                    tarih = table.Column<DateTime>(nullable: true),
                    okundu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kullar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eposta = table.Column<string>(maxLength: 60, nullable: false),
                    epostaEnc = table.Column<string>(maxLength: 600, nullable: false),
                    password = table.Column<string>(maxLength: 12, nullable: false),
                    passwordEnc = table.Column<string>(maxLength: 120, nullable: false),
                    cerez = table.Column<string>(maxLength: 60, nullable: true),
                    cerezEnc = table.Column<string>(maxLength: 600, nullable: true),
                    epostaCode = table.Column<string>(maxLength: 60, nullable: true),
                    epostaCodeEnc = table.Column<string>(maxLength: 600, nullable: true),
                    epostaOnay = table.Column<bool>(nullable: false),
                    beniHatirla = table.Column<bool>(nullable: false),
                    hatali = table.Column<int>(nullable: false),
                    kilitli = table.Column<bool>(nullable: false),
                    role = table.Column<int>(nullable: false),
                    uyelikTarihi = table.Column<DateTime>(nullable: true),
                    silindi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sayfa",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anatab = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 500, nullable: true),
                    thumb = table.Column<string>(maxLength: 500, nullable: true),
                    icerik = table.Column<string>(nullable: true),
                    yayinTarihi = table.Column<DateTime>(nullable: true),
                    aktif = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sayfa", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Kullar");

            migrationBuilder.DropTable(
                name: "Sayfa");
        }
    }
}
