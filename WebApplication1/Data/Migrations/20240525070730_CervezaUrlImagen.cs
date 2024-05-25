using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class CervezaUrlImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImagen",
                table: "Cervezas",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Cervezas");
        }
    }
}
