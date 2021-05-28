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


    }
}
