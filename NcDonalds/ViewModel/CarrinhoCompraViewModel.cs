using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class CarrinhoCompraViewModel
    {
        public CarrinhoCompra CarrinhoCompra { get; set; }
        public decimal CarrinhoCompraTotal { get; set; }

        [Display(Name = "Código do Cupom")]
        public string codCupom { get; set; }

        public int idCupom { get; set; }
    }
}
