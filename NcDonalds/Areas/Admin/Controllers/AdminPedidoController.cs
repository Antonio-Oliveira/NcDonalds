using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminPedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdminPedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index()
        {
            var pedidos = _pedidoRepository.GetPedidosPendentes();
            return View(pedidos);
        }

        public IActionResult HistoricoPedidos()
        {
            var pedidos = _pedidoRepository.GetPedidos();
            return View(pedidos);
        }

        public async Task<IActionResult> ConfirmarPedido(int? pedidoId)
        {
            if(pedidoId == null)
            {
                return NotFound();
            }

            var result = await _pedidoRepository.ConfirmarPedido((int)pedidoId);

            if (!result)
            {
                ModelState.AddModelError("","Criação de lanche não finalizada");
            }

            return RedirectToAction("Index");

        }


    }
}
