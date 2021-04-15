using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class PopularTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Sanduíche','Um composição de pão, carnes, queijos e verduras')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Bebidas','Refrigerantes, sucos e batidas')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Acompanhamentos','Fritos, Molhos e Saladas')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
