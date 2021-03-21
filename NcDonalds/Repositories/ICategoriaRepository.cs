using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    interface ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorias { get; }

    }
}
