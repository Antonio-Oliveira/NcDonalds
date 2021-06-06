using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IPedidoRepository _pedidoRepository;

        public AccountController(IAppUserRepository appUserRepository, CarrinhoCompra carrinhoCompra, IPedidoRepository pedidoRepository)
        {
            _appUserRepository = appUserRepository;
            _carrinhoCompra = carrinhoCompra;
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _appUserRepository.GetUserById(userId);


            if (user != null)
            {
                var profileVM = new ProfileViewModel()
                {
                    Cpf = user.Cpf,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    Telefone = user.PhoneNumber,
                    UserName = user.UserName
                };
                return View(profileVM);
            }

            ModelState.AddModelError("", "Usuário vazio");
            return RedirectToAction("Index", "Home");

        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var login = await _appUserRepository.Login(loginVM);

            if (login)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuário ou Senha não encontrados");
            return View(loginVM);

        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel registroVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Completa o formulario");
                return View(registroVM);
            }

            if (registroVM.Password != registroVM.Confirme_Password)
            {
                ModelState.AddModelError("", "Senha de confirmação diferente");
                return View(registroVM);
            }

            var register = await _appUserRepository.Register(registroVM);

            if (!register)
            {
                ModelState.AddModelError("", "Erro: Conta não criada");
                return View(registroVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            _appUserRepository.Logout();
            _carrinhoCompra.LimparCarrinho();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Enderecos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var enderecos = _appUserRepository.GetEnderecosByUserId(userId);
            return View(enderecos);
        }

        [HttpGet]
        public IActionResult AdicionarEndereco()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AdicionarEndereco(Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Campos do formulario preenchidos incorretamente");
                return View(endereco);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            endereco.UserId = userId;
            var result = await _appUserRepository.AddEndereco(endereco);

            if (!result)
            {
                ModelState.AddModelError("","Erro ao cadastrar Endereço");
                return View(endereco);
            }

            return RedirectToAction("Enderecos", "Account");
        }

        [HttpGet]
        public IActionResult EditEndereco(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var endereco = _appUserRepository.GetEnderecosById((int)id);
            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> EditEndereco(Endereco endereco)
        {

            if (!ModelState.IsValid){
                ModelState.AddModelError("","Campos do formulario preenchidos incorretamente");
                return View(endereco);
            }

            var result = await _appUserRepository.UpdateEndereco(endereco);

            if (!result)
            {
                ModelState.AddModelError("", "Erro ao atualizar endereço");
                return View(endereco);
            }

            return RedirectToAction("Enderecos", "Account");
        }

        public async Task<IActionResult> RemoverEndereco(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var result = await _appUserRepository.RemoveEndereco((int)id);
            return RedirectToAction("Enderecos", "Account");
        }

        [HttpGet]
        public IActionResult Pedidos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pedidos = _pedidoRepository.GetUserPedidos(userId);
            return View(pedidos);
        }

    }
}
