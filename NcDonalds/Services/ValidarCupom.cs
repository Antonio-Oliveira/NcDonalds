using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.services
{
    public class ValidarCupom
    {
        private readonly AppDbContext _context;
        private readonly IPedidoRepository _pedidoRepository;

        public ValidarCupom(AppDbContext context, IPedidoRepository pedidoRepository)
        {
            _context = context;
            _pedidoRepository = pedidoRepository;
        }


        public bool validar(string userId, string cupomName, CarrinhoCompra cp)
        {
            if (string.IsNullOrEmpty(cupomName))
            {
                cupomName = cupomName.ToUpper();

                var cupom = _context.Cupons.FirstOrDefault(c => c.Nome.Equals(cupomName));

                if (cupom != null)
                {
                    if (!((decimal)cupom.CompraMinima <= cp.GetCarrinhoCompraTotal()))
                        return false;

                    if (cupom.CompraMaxima != 0)
                    {
                        if (!((decimal)cupom.CompraMaxima >= cp.GetCarrinhoCompraTotal()))
                        {
                            return false;
                        }
                    }

                    if (!(cupom.Vencimento >= DateTime.Now))
                        return false;

                    if (cupom.PrimeiraPedido)
                    {
                       var isValid = _pedidoRepository.GetUserPedidos(userId);

                        if (isValid)
                        {
                            return false;
                        }
                    }

                    return true;

                }

            }

            return false;
        }

    }
}
