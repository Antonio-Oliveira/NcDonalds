using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Display(Name = "Lanches do Pedido")]
        public CarrinhoCompra Carrinho { get; set; }

        [Display(Name = "Endereço da entrega")]
        public Endereco EnderecoPedido { get; set; }

        [Display(Name = "Endereços pré-cadastrados pelo usuário")]
        public IEnumerable<Endereco> EnderecoUser { get; set; }

        [Display(Name = "Preço total do carrinho")]
        public decimal TotalCarrinho { get; set; }

        [Display(Name = "Preço total do pedido")]
        public decimal TotalFinal { get; set; }

        [Display(Name = "Código do Cupom")]
        public string codCupom { get; set; }

        public int idCupom { get; set; }
    }
}
