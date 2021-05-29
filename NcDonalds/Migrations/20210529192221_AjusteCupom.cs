using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class AjusteCupom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cupons");

            migrationBuilder.AddColumn<string>(
                name: "CodigoCupom",
                table: "Cupons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Cupons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoCupom",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Cupons");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cupons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
