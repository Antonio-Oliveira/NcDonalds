using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NcDonalds.Models;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            try
            {
                if (!ModelState.IsValid)
                    return View(loginVM);

                var user = await _userManager.FindByEmailAsync(loginVM.Email);

                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }

                    ModelState.AddModelError("","Usuário ou Senha não encontrados");
                    return View(loginVM);
                }


                ModelState.AddModelError("", "Usuário ou Senha não encontrados");
                return View(loginVM);
            }
            catch
            {
                return View();
            }
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
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Usuário não cadastrado");
                    return View(registroVM);
                }

                if(registroVM.Password != registroVM.Confirme_Password)
                {
                    ModelState.AddModelError("", "Senha de confirmação diferente");
                    return View(registroVM);
                }   

                var user = new AppUser()
                {
                    UserName = registroVM.UserName,
                    Cpf = registroVM.Cpf,
                    Email = registroVM.Email,
                    PhoneNumber = registroVM.Telefone
                };

                var result = await _userManager.CreateAsync(user,registroVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Erro: Conta não criada");
                return View(registroVM);
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


    }
}
