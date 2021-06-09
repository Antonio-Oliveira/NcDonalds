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
        public CarrinhoCompra lanches { get; set; }

        public decimal totalCarrinho { get; set; }

        [Display(Name = "Código do Cupom")]
        public string codCupom { get; set; }

        public Endereco enderecoPedido { get; set; }

        public IEnumerable<Endereco> enderecoUser { get; set; }
        
        public int idCupom { get; set; }

    }
}
