using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Lanche
    {
        public int Preco { get; set; }
        public string Nome { get; set; }


        // Novos atributos
        public int LancheId { get; set; }
        public string ImagemURL { get; set; }

    }
}
