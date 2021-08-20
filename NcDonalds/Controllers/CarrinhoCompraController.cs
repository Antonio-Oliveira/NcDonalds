using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.Services.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICupomService _cupomService;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra, IPedidoRepository PedidoRepository, AppDbContext Context, IAppUserRepository appUserRepository, ICupomService cupomService)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
            _appUserRepository = appUserRepository;
            _cupomService = cupomService;
        }

        public IActionResult Index()
        {
            var carrinhoCompraVM = new CarrinhoCompraViewModel()
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [HttpPost]
        public async Task<decimal> AdicionarItem(int lancheId)
        {
            var lancheSelecionado = await _lancheRepository.GetLancheById(lancheId);

            if (lancheSelecionado != null)
            {
                await _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado, 1);
            }

            return _carrinhoCompra.GetCarrinhoTotalItens();
        }

        [HttpPost]
        public async Task<decimal> RemoverItem(int lancheId)
        {
            var lancheSelecionado = await _lancheRepository.GetLancheById(lancheId);

            if (lancheSelecionado != null)
            {
                await _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return _carrinhoCompra.GetCarrinhoTotalItens();
        }

        [HttpPost]
        public IActionResult ValidarCupom(string codigoCupom)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Cupom Invalido");
                return Json("Cupom Invalido");
            }

            if (string.IsNullOrEmpty(codigoCupom))
            {
                ModelState.AddModelError("", "Cupom Invalido");
                return Json("Cupom Invalido");
            }

            var cupom = _cupomService.ValidarCupom(codigoCupom, _carrinhoCompra, userId);

            if (cupom == null)
            {
                return NotFound("Cupom inválido");
            }

            return Json(cupom);
        }
    }
}