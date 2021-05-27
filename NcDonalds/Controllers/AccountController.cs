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
    public class AccountController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;

        public AccountController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [Authorize]
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
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return RedirectToAction("Index", "Home");
        }


    }
}
