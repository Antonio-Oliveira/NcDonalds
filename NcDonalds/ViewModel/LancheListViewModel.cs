using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }

        public string CategoriaAtual { get; set; }

    }
}
