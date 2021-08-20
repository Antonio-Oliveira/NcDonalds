using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoService(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public async Task AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            await _carrinhoCompra.AdicionarAoCarrinho(lanche, 1);
        }

        public async Task RemoverNoCarrinho(Lanche lanche)
        {
            await _carrinhoCompra.RemoverDoCarrinho(lanche);
        }


        public Task<IActionResult> GetCarrinhoCompra()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetCarrinhoCompraTotal()
        {
            throw new NotImplementedException();
        }

       
    }
}
