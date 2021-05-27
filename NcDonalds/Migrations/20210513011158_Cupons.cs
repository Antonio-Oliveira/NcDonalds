using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class Cupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemThumbURL",
                table: "Lanches");

            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    CupomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    CompraMinima = table.Column<double>(nullable: false),
                    CompraMaxima = table.Column<double>(nullable: false),
                    Emissão = table.Column<DateTime>(nullable: false),
                    Vencimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.CupomId);
                });

            migrationBuilder.CreateTable(
                name: "UserCupons",
                columns: table => new
                {
                    UserCupomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CupomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCupons", x => x.UserCupomId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropTable(
                name: "UserCupons");

            migrationBuilder.AddColumn<string>(
                name: "ImagemThumbURL",
                table: "Lanches",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
