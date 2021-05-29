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

        public Cupom validarCupom(string codigoCupom)
        {
            var cupom = _cupomRepository.GetCupomByName(codigoCupom);

            if(cupom != null)
            {
                return cupom;
            }

            return null;
        }

    }
}
