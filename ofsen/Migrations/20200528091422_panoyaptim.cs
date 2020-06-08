using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ofsen.Migrations
{
    public partial class panoyaptim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aktif",
                table: "Sayfa");

            migrationBuilder.DropColumn(
                name: "anatab",
                table: "Sayfa");

            migrationBuilder.DropColumn(
                name: "thumb",
                table: "Sayfa");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Sayfa");

            migrationBuilder.DropColumn(
                name: "yayinTarihi",
                table: "Sayfa");

            migrationBuilder.CreateTable(
                name: "Pano",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anatab = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 500, nullable: true),
                    thumb = table.Column<string>(maxLength: 500, nullable: true),
                    metin = table.Column<string>(maxLength: 5000, nullable: true),
                    yayinTarihi = table.Column<DateTime>(nullable: true),
                    aktif = table.Column<bool>(nullable: false),
                    sayfaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pano", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pano_Sayfa_sayfaID",
                        column: x => x.sayfaID,
                        principalTable: "Sayfa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pano_sayfaID",
                table: "Pano",
                column: "sayfaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pano");

            migrationBuilder.AddColumn<bool>(
                name: "aktif",
                table: "Sayfa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "anatab",
                table: "Sayfa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "thumb",
                table: "Sayfa",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Sayfa",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "yayinTarihi",
                table: "Sayfa",
                type: "datetime2",
                nullable: true);
        }
    }
}
