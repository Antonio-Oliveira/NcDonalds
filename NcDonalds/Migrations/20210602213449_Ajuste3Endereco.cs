using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class Ajuste3Endereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Enderecos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Enderecos");
        }
    }
}
