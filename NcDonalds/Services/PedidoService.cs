using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.services
{
    public class PedidoService
    {
        private readonly ICupomRepository _cupomRepository;

        public PedidoService(ICupomRepository cupomRepository)
        {
            _cupomRepository = cupomRepository;
        }

        public Cupom validarCupom(string codigoCupom, CarrinhoCompra carrinho, string userId)
        {
            var cupom = _cupomRepository.GetCupomByName(codigoCupom);

            if(cupom != null)
            {
               if(cupom.Vencimento != null && cupom.Vencimento > DateTime.Now)
                {
                    if (cupom.CompraMinima != 0 && cupom.CompraMinima >= carrinho.GetCarrinhoCompraTotal())
                    {
                        if(cupom.CompraMaxima != 0 && cupom.CompraMaxima <= carrinho.GetCarrinhoCompraTotal())
                        {
                            
                        }
                    }
                }



                return cupom;
            }

            return null;
        }

    }
}
