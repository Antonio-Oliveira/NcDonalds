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

        IEnumerable<Pedido> GetPedidos();

        void CriarPedido(Pedido pedido);
        
        IEnumerable<Pedido> GetPedidosPendentes();

        Task<bool> ConfirmarPedido(int pedidoId);

        bool GetUserPedidos(string userName);

    }
}
