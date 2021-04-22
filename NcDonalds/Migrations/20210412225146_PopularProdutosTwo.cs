using Microsoft.EntityFrameworkCore.Migrations;

namespace NcDonalds.Migrations
{
    public partial class PopularProdutosTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbURL,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Bebidas'),'Leite, Sorvete de creme, Chocolate e Oreo','MilkShake de chocolate com Oreo',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','Milkshake de Oreo', 21.00)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbURL,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Bebidas'),'Coca-Cola','Coca-Cola',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','Coca-Cola 2l', 10.00)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbURL,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Acompanhamentos'),'Fritas','Fritas',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','Batata Frita G', 6.00)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbURL,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Acompanhamentos'),'Nuggets','Nuggets',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','Nuggets', 7.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
