using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSuperMarket.Migrations
{
    public partial class stok_durumu_eklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StoktaVarMi",
                table: "Urunler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoktaVarMi",
                table: "Urunler");
        }
    }
}
