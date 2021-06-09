using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class CheckoutViewModel
    {
        public List<CarrinhoCompraItem> itens { get; set; }

        [Display(Name = "Código do Cupom")]
        public string codCupom { get; set; }

        public Endereco enderecoPedido { get; set; }

        public IEnumerable<Endereco> enderecoUser { get; set; }

        public decimal total { get; set; }

        public decimal pedidoTotal { get; set; }

        public decimal desconto { get; set; }

    }
}
