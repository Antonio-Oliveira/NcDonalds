using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }

        public int PedidoId { get; set; }

        public int LancheId { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco { get; set; }

        public virtual Lanche Lanche { get; set; }

        public virtual Pedido Pedido { get; set; }

    }
}
