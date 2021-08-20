using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<decimal> GetCarrinhoCompraTotal();
        Task<IActionResult> GetCarrinhoCompra();
        Task AdicionarAoCarrinho(Lanche lanche, int quantidade);
        Task RemoverNoCarrinho(Lanche lanche);
    }
}