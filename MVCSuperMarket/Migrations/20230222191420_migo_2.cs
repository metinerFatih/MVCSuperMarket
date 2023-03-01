using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSuperMarket.Migrations
{
    public partial class migo_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarihi",
                table: "Urunler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarihi",
                table: "Urunler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EklenmeTarihi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "GuncellenmeTarihi",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Urunler");
        }
    }
}
