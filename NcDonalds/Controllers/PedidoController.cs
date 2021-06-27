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
        private readonly ICupomRepository _cupomRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra, IAppUserRepository appUserRepository, PedidoService pedidoService, ICupomRepository cupomRepository, IEnderecoRepository enderecoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _appUserRepository = appUserRepository;
            _pedidoService = pedidoService;
            _cupomRepository = cupomRepository;
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEnderecos = _appUserRepository.GetEnderecosByUserId(userId);

            var checkoutVM = new CheckoutViewModel()
            {
                Carrinho = _carrinhoCompra,
                TotalCarrinho = _carrinhoCompra.GetCarrinhoCompraTotal(),
                EnderecoUser = userEnderecos,
                TotalFinal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(checkoutVM);
        }

        [HttpPost]
        public IActionResult ValidarCupom(string codigoCupom)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Cupom Invalido");
                //return RedirectToAction("Checkout", "Pedido");
                return Json("Cupom Invalido");
            }

            if (string.IsNullOrEmpty(codigoCupom))
            {
                ModelState.AddModelError("", "Cupom Invalido");
                return Json("Cupom Invalido");
            }

            var cupom = _pedidoService.validarCupom(codigoCupom, _carrinhoCompra, userId);

            if(cupom == null)
            {
                return NotFound("Cupom inválido");
            }

            return Json(cupom);
        }

        [HttpPost]
        public IActionResult CheckoutFinal(CheckoutViewModel checkoutVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);
            var cupom = _cupomRepository.GetCupomById(checkoutVM.idCupom);

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
                pedidoTotal = pedidoTotal - cupom.Valor;
            }

            Pedido pedido = new Pedido()
            {
                PedidoTotal = pedidoTotal,
                TotalItensPedido = pedidoTotalItens,
                UserId = userId,
            };

            if (cupom != null)
            {
                pedido.CupomId = cupom.CupomId;
            }


            _pedidoRepository.CriarPedido(pedido);
            _carrinhoCompra.LimparCarrinho();

            var pedidoVM = new PedidoViewModel()
            {
                Itens = itens,
                User = user,
                pedido = pedido,
            };

            if (cupom != null)
            {
                pedidoVM.cupom = cupom;
            }

            if (ModelState.IsValid)
            {
                return View(checkoutVM);
            }

            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedidoVM);


        }

        public IActionResult CheckoutCompleto(PedidoViewModel pedidoVM)
        {
            return View(pedidoVM);
        }


    }
}
