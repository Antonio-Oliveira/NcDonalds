using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public AdminLancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lanches = _lancheRepository.Lanches;
            return View(lanches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                var result = await _lancheRepository.AddLanche(lanche);

                if (result)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(lanche);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            var lanche = _lancheRepository.GetLancheById((int)id);

            if(lanche == null)
            {
                ModelState.AddModelError("", "Lanche não encontrado");
                return RedirectToAction("Index");
            }

            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View(lanche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemURL,ImagemThumbURL,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                var result =  await _lancheRepository.UpdateLanche(lanche);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("","Lanche não criado");

            }

            return View(lanche);
        }

        public async Task<IActionResult> Delete(int? id)
        { 
            if(id == null)
            {
                ModelState.AddModelError("","Id do lanche vázio");
                return RedirectToAction("Index");
            }

            var result = await _lancheRepository.EmEstoque((int)id);

            if (!result)
            {
                ModelState.AddModelError("", "Erro ao tirar lanche do estoque");
                return RedirectToAction("Index", "AdminLanche");
            }

            return RedirectToAction("Index", "AdminLanche");
        }
    }
}
