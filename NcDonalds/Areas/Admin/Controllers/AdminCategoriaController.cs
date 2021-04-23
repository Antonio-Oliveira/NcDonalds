using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class AdminCategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public AdminCategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _categoriaRepository.Categorias;
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoriaRepository.AddCategoria(categoria);

                if (result)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var categoria = _categoriaRepository.GetCategoriaById((int)id);

            if (categoria == null)
            {
                ModelState.AddModelError("", "categoria não encontrado");
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoriaRepository.UpdateCategoria(categoria);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Lanche não modificado");

            }

            return View(categoria);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var categoria = _categoriaRepository.GetCategoriaById((int)id);

            if(categoria != null)
            {
                return View(categoria);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int categoriaId)
        {

            var result = await _categoriaRepository.RemoveCategoria(categoriaId);

            if (result)
            {
                return RedirectToAction("Index", "AdminLanche");
            }

            return View(categoriaId);
        }

    }
}
