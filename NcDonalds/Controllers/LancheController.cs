using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.Services.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheService _lancheService;
        private readonly ICategoriaService _categoriaService;

        public LancheController(ILancheService lancheService, ICategoriaService categoriaService)
        {
            _lancheService = lancheService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<LancheListViewModel>> List([FromRoute] string categoria)
        {
            try
            {
                var categorias = await _categoriaService.GetCategorias();
                var lanchesListViewModel = await _lancheService.LancheList(categoria, categorias);
                return View(lanchesListViewModel);
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return RedirectToAction("Index", "Home");
            }

        }

    }
}
