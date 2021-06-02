using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.services;
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
        private readonly PedidoService _pedidoService;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra, IAppUserRepository appUserRepository, PedidoService pedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _appUserRepository = appUserRepository;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public IActionResult Checkout(Cupom cupom)
        {
            List<CarrinhoCompraItem> itensCarinho = _carrinhoCompra.GetCarrinhoCompraItens();

            var checkoutVM = new CheckoutViewModel()
            {
                itens = itensCarinho,
                cupom = cupom
            };

            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> ValidarCupom(string codigoCupom)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Cupom Invalido");
                return RedirectToAction("Checkout");
            }

            if(string.IsNullOrEmpty(codigoCupom))
            {
                ModelState.AddModelError("", "Cupom Invalido");
                return RedirectToAction("Checkout");
            }

            var cupom = _pedidoService.validarCupom(codigoCupom, _carrinhoCompra, userId);
            
            return RedirectToAction("Checkout", "Pedido", cupom);
        }

        [HttpPost]
        public IActionResult CheckoutFinal(CheckoutViewModel checkoutVM)
        {
            var teste = checkoutVM;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);
            var cupom = checkoutVM.cupom;

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
                return RedirectToAction("Index", "CarrinhoCompra");
            }

            foreach (var item in itens)
            {
                pedidoTotalItens += item.Quantidade;
                pedidoTotal += item.Lanche.Preco * item.Quantidade;
            }

            if (cupom != null)
            {
                if (cupom.Tipo.Equals("Porcetagem"))
                {
                    pedidoTotal = pedidoTotal - (pedidoTotal * cupom.Valor / 100);
                }
                else
                {
                    pedidoTotal = pedidoTotal - cupom.Valor;
                }

                pedidoTotal = pedidoTotal >= 0 ? pedidoTotal : pedidoTotal = 0;
            }

            

            Pedido pedido = new Pedido()
            {
                PedidoTotal = pedidoTotal,
                TotalItensPedido = pedidoTotalItens,
                UserId = userId,
                CupomId = cupom.CupomId
            };

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
