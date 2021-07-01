using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Services.Interfaces
{
    public interface ICupomService
    {
        Cupom ValidarCupom(string codigoCupom, CarrinhoCompra carrinho, string userId);
        decimal CalcularCupom(Cupom cupom, decimal PrecoPedido);

    }
}
