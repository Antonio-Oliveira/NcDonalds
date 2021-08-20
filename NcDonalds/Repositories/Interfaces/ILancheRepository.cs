using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        Task<List<Lanche>> GetLanches();

        Task<Lanche> GetLancheById(int lancheId);

        Task<bool> AddLanche(Lanche lanche);

        Task<bool> EmEstoque(int lancheId);

        Task<bool> UpdateLanche(Lanche lanche);
    }
}
