using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Pedido GetPedidoById(int pedidoId);

        List<Pedido> GetPedidos();

        void CriarPedido(Pedido pedido);
    }
}
