using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface ICupomRepository
    {

        Task<bool> AddCupom(Cupom cupom);

        Task<bool> RemoveCupom(int cupomId);

        Task<bool> UpdateCupom(Cupom cupom);

        Cupom GetCupomByName(string codigoCupom);

        IEnumerable<Cupom> GetCupons { get; }

    }
}
