using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class PopularCupom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Cupons(Nome,Tipo,Valor,CompraMinima,CompraMaxima,Emissão,Vencimento) VALUES('BEM-VINDO','Porcentagem','30',40,'100','2021-05-12 16:03:15','2021-06-12 16:03:15')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cupons");
            migrationBuilder.Sql("DELETE FROM UserCupons");
        }
    }
}
