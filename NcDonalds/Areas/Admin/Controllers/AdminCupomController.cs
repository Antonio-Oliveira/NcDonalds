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
    public class AdminCupomController : Controller
    {
        private readonly ICupomRepository _cupomRepository;

        public AdminCupomController(ICupomRepository cupomRepository)
        {
            _cupomRepository = cupomRepository;
        }

        public IActionResult Index()
        {
            var cupons = _cupomRepository.GetCupons;
            return View(cupons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cupom cupom)
        {
            if (ModelState.IsValid)
            {
                var result = await _cupomRepository.AddCupom(cupom);

                if (result)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(cupom);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cupom = _cupomRepository.GetCupomById((int)id);

            if (cupom == null)
            {
                ModelState.AddModelError("", "Cupom não encontrado");
                return RedirectToAction("Index");
            }

            return View(cupom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cupom cupom)
        {
            if (ModelState.IsValid)
            {
                var result = await _cupomRepository.UpdateCupom(cupom);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Cupom não criado");

            }

            return View(cupom);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _cupomRepository.RemoveCupom((int)id);

            if (result)
            {
                return RedirectToAction("Index", "AdminCupom");
            }

            return RedirectToAction("Index", "AdminCupom");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = _cupomRepository.GetCupomById((int)id);

            if (cupom == null)
            {
                ModelState.AddModelError("","Cupom não encontrado");
                return RedirectToAction("Index", "AdminCupom");
            }

            return View(cupom);
        }
    }
}
