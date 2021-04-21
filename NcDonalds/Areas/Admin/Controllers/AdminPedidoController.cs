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
    public class AdminPedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdminPedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index()
        {
            var pedidos = _pedidoRepository.GetPedidos();
            return View(pedidos);
        }


    }
}
