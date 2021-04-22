using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IAppUserRepository _appUserRepository;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra, IAppUserRepository appUserRepository)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _appUserRepository = appUserRepository;
        }

        public IActionResult Checkout(Pedido pedido)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);

            if (user == null)
            {
                ModelState.AddModelError("", "Erro em verificar o usuário");
                return RedirectToAction("Index", "CarrinhoCompra");
            }

            decimal pedidoTotal = 0.0m;
            int pedidoTotalItens = 0;
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();

            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho estar vazio, inclua um lanche");
            }

            foreach (var item in itens)
            {
                pedidoTotalItens += item.Quantidade;
                pedidoTotal += item.Lanche.Preco * item.Quantidade;
            }

            pedido.PedidoTotal = pedidoTotal;
            pedido.TotalItensPedido = pedidoTotalItens;
            pedido.UserId = user.UserName;

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                _carrinhoCompra.LimparCarrinho();

                var pedidoVM = new PedidoViewModel()
                {
                    Itens = itens,
                    User = user,
                    pedido = pedido
                };


                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedidoVM);
            }

            return View(pedido);
        }

        public IActionResult CheckoutCompleto()
        {
            return View();
        }


    }
}
