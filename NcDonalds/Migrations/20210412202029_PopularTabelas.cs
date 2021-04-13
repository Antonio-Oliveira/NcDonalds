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

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Sanduíche'),'Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',1, 'https://drive.google.com/file/d/1_YtkmFa2Z5gGwO7Xmm3VemXlTbDJ0fm2/view?usp=sharing','https://drive.google.com/file/d/1_YtkmFa2Z5gGwO7Xmm3VemXlTbDJ0fm2/view?usp=sharing','Cheese Salada', 12.50)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Sanduíche'),'Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.',1,'http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','Misto Quente', 8.00)");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,Nome,Preco) VALUES((SELECT CategoriaId FROM Categorias Where Nome='Sanduíche'),'Pão, hambúrger, presunto, mussarela e batalha palha','Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','Cheese Burger', 11.00)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
