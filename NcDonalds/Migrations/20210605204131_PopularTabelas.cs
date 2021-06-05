using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class PopularTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Sanduiche','Um composição de pão, carnes, queijos e verduras')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Bebidas','Refrigerantes, sucos e batidas')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) VALUES('Acompanhamentos','Fritos, Molhos e Saladas')");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemURL,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Sanduiche'),'Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',1,'/images/Lanches/cheeseburguer.jpg','Cheese Salada', 12.50)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Sanduiche'),'Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.',1,'/images/Lanches/cheeseburguer.jpg','Misto Quente', 8.00)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemURL,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Bebidas'),'Refri','Refrigerante de qualquer tipo',1,'/images/Lanches/cheeseburguer.jpg','Refri', 12.50)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Bebidas'),'Milk Shake','Milk Shake',1,'/images/Lanches/cheeseburguer.jpg','Milk Shake', 8.00)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemURL,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Acompanhamentos'),'Nuggets','Delicioso Nuggets',1,'/images/Lanches/cheeseburguer.jpg','Nuggets', 12.50)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Acompanhamentos'),'Batata','Delicioso Nuggets',1,'/images/Lanches/cheeseburguer.jpg','Batata', 8.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
