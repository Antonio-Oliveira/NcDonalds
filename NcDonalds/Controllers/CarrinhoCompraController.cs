using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using NcDonalds.Repositories;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel()
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };


            return View(carrinhoCompraVM);
        }

        public RedirectToActionResult AdicionarItem(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault( l => l.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItem(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
