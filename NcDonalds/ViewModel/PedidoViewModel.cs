using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class PedidoViewModel
    {
        public List<CarrinhoCompraItem> Itens { get; set; }

        public AppUser User { get; set; }

        public Pedido pedido { get; set; }

        public Cupom cupom { get; set; }

    }
}
