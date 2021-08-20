using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.Services.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Services
{
    public class LancheService : ILancheService
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheService(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public async Task<LancheListViewModel> LancheList(string categoria, List<Categoria> categorias)
        {
            var lanches = await _lancheRepository.GetLanches();
            var categoriaAtual = string.Empty;

            if (!string.IsNullOrEmpty(categoria))
            {
                foreach (var item in categorias)
                {
                    if (item.Nome.ToLower().Equals(categoria.ToLower()))
                    {
                        categoriaAtual = item.Nome;
                        lanches = lanches.Where(l => l.Categoria.Nome == categoriaAtual).ToList();
                        break;
                    }

                    categoriaAtual = "Categoria não existe";
                }
            }

            return new LancheListViewModel()
            {
                CategoriaAtual = categoriaAtual,
                Categorias = categorias,
                Lanches = lanches
            };
        }
    }
}