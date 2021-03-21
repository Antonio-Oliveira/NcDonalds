using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        public IEnumerable<Lanche> Lanches => throw new NotImplementedException();

        public Lanche GetLancheById(int lancheId)
        {
            throw new NotImplementedException();
        }
    }
}
