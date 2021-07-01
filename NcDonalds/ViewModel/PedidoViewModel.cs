using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class PedidoViewModel
    {
        public List<CarrinhoCompraItem> Itens { get; set; }

        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Display(Name = "Pedido:")]
        public Pedido Pedido { get; set; }

        [Display(Name = "Cupom:")]
        public Cupom Cupom { get; set; }

    }
}
