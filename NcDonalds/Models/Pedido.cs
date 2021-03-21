using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public int LancheId { get; set; }

        public int Numero { get; set; }
        public string Valor{ get; set; }
    }
}
