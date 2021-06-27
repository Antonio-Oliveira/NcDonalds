using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List(string categoria)
        {
            string categoriaAtual = string.Empty;
            var categorias = _categoriaRepository.Categorias;
            IEnumerable<Lanche> lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);

            if (!(string.IsNullOrEmpty(categoria) || string.IsNullOrWhiteSpace(categoria)))
            {
                foreach (var value in categorias)
                {
                    if (value.Nome.ToLower().Equals(categoria.ToLower()))
                    {
                        lanches = _lancheRepository.Lanches.Where(l => l.Categoria.Nome.Equals(categoria)).OrderBy(l => l.Nome);
                        categoriaAtual = categoria;
                        break;
                    }
                }

            }

            if ((string.IsNullOrEmpty(categoriaAtual) || string.IsNullOrWhiteSpace(categoriaAtual)))
            {
                categoriaAtual = "OS LANCHES QUE VOCÊ AMA";
            }

            var lanchesListViewModel = new LancheListViewModel()
            {
                CategoriaAtual = categoriaAtual,
                Lanches = lanches,
                Categorias = categorias
            };

            return View(lanchesListViewModel);
        }

    }   
}
