using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class CheckoutViewModel
    {
        public List<CarrinhoCompraItem> itens { get; set; }

        public Cupom cupom { get; set; }

        public Endereco enderecoPedido { get; set; }

        public IEnumerable<Endereco> enderecoUser { get; set; }

        public string tipoEntrega { get; set; }

        public string formaPagamento { get; set; }

    }
}
