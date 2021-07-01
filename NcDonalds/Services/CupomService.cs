using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.services
{
    public class CupomService : ICupomService
    {
        private readonly ICupomRepository _cupomRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public CupomService(ICupomRepository cupomRepository, IPedidoRepository pedidoRepository)
        {
            _cupomRepository = cupomRepository;
            _pedidoRepository = pedidoRepository;
        }

        public Cupom ValidarCupom(string codigoCupom, CarrinhoCompra carrinho, string userId)
        {
            var cupom = _cupomRepository.GetCupomByName(codigoCupom);
            var pedidos = _pedidoRepository.GetUserPedidos(userId);

            if (cupom != null)
            {
                if (cupom.Vencimento != null)// && cupom.Vencimento > DateTime.Now) || cupom.Vencimento == null)
                {
                    if ((cupom.PrimeiroPedido && pedidos.Count() == 0) || (cupom.PrimeiroPedido == false))
                    {
                        if ((cupom.CompraMinima != 0 && cupom.CompraMinima >= carrinho.GetCarrinhoCompraTotal()) || cupom.CompraMinima == 0)
                        {
                            if ((cupom.CompraMaxima != 0 && cupom.CompraMaxima <= carrinho.GetCarrinhoCompraTotal()) || cupom.CompraMaxima == 0)
                            {
                                return cupom;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public decimal CalcularCupom(Cupom cupom, decimal PrecoPedido)
        {
            if (cupom != null)
            {
                if (cupom.Tipo.Equals("PORCENTAGEM"))
                {
                    return PrecoPedido - (PrecoPedido * cupom.Valor / 100);
                }

                return PrecoPedido - cupom.Valor;

            }

            return 0;
        }

     
    }
}
