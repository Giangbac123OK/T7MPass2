using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sotaikhoan",
                table: "trahangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tennganhang",
                table: "trahangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tentaikhoan",
                table: "trahangs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sotaikhoan",
                table: "trahangs");

            migrationBuilder.DropColumn(
                name: "Tennganhang",
                table: "trahangs");

            migrationBuilder.DropColumn(
                name: "Tentaikhoan",
                table: "trahangs");
        }
    }
}
