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

        public string cupomName { get; set; }

    }
}
