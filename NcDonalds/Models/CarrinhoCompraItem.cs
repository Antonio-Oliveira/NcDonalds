using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class CarrinhoCompraItem
    {
        //Chave Primaria
        public int CarrinhoCompraItemId { get; set; }

        public Lanche Lanche { get; set; }

        public int Quantidade { get; set; }

        public string CarrinhoCompra { get; set; }
    }
}
