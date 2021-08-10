using NcDonalds.Models;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Services.Interfaces
{
    public interface ILancheService
    {
        Task<LancheListViewModel> LancheList(string categoria, List<Categoria> categorias);
    }
}
