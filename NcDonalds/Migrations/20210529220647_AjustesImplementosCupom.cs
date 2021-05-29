using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class AjustesImplementosCupom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCupons");

            migrationBuilder.DropColumn(
                name: "PrimeiraPedido",
                table: "Cupons");

            migrationBuilder.AddColumn<bool>(
                name: "PrimeiroPedido",
                table: "Cupons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimeiroPedido",
                table: "Cupons");

            migrationBuilder.AddColumn<bool>(
                name: "PrimeiraPedido",
                table: "Cupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserCupons",
                columns: table => new
                {
                    UserCupomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CupomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCupons", x => x.UserCupomId);
                });
        }
    }
}
