using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        Lanche GetLancheById(int lancheId);
    }
}
