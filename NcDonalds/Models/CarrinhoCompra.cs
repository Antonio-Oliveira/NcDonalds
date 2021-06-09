using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NcDonalds.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _context = contexto;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto 
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem =
                    _context.CarrinhoCompraItens.FirstOrDefault(
                        cp => cp.Lanche.LancheId == lanche.LancheId && cp.CarrinhoCompraId == CarrinhoCompraId);

            //verifica se o carrinho existe e senão cria um
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            //se existir o carrinho com o item então incrementa a quantidade
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();

        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault( 
                cp => cp.Lanche.LancheId == lanche.LancheId && cp.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            
            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();
            return quantidadeLocal;

        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = 
                _context.CarrinhoCompraItens.Where(
                    cp => cp.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(l => l.Lanche).ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens.Where(ci => ci.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            return _context.CarrinhoCompraItens
                .Where(cp => cp.CarrinhoCompraId == CarrinhoCompraId)
                    .Select(q => q.Lanche.Preco * q.Quantidade)
                        .Sum();
        }

        public int GetCarrinhoTotalItens()
        {
            return _context.CarrinhoCompraItens
                .Where(cp => cp.CarrinhoCompraId == CarrinhoCompraId)
                    .Select(l => l.Quantidade)
                        .Sum();
        }



    }
}
