using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using NcDonalds.Repositories;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
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
            string _categoria = categoria;
            string categoriaAtual = string.Empty;
            IEnumerable<Lanche> lanches;

            if (string.IsNullOrEmpty(_categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "OS BURGERS QUE VOCÊ AMA";
            }
            else
            {
                switch (_categoria)
                {
                    case "Sanduíche":
                        lanches = _lancheRepository.Lanches.Where(l => l.Categoria.Nome.Equals("Sanduíche")).OrderBy(l => l.Nome);
                        categoriaAtual = "Sanduíche";
                        break;

                    case "Bebidas":
                        lanches = _lancheRepository.Lanches.Where(l => l.Categoria.Nome.Equals("Bebidas")).OrderBy(l => l.Nome);
                        categoriaAtual = "Bebidas";
                        break;

                    case "Acompanhamentos":
                        lanches = _lancheRepository.Lanches.Where(l => l.Categoria.Nome.Equals("Acompanhamentos")).OrderBy(l => l.Nome);
                        categoriaAtual = "Acompanhamentos";
                        break;

                    default:
                        lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                        categoriaAtual = "Categoria não encontrada";
                        break;
                }
            }

            var lanchesListViewModel = new LancheListViewModel()
            {
                CategoriaAtual = categoriaAtual,
                Lanches = lanches
            };
            return View(lanchesListViewModel);
        }
    }
}
