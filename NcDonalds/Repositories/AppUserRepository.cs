using Microsoft.AspNetCore.Identity;
using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserRepository(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public AppUser GetUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == userName);
            return user;
        }

        public AppUser GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == id);
            return user;
        }

        public async Task<bool> Login(LoginViewModel loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return true;
                }
            }

            return false;

        }


        public async Task<bool> Register(RegisterViewModel registroVM)
        {
            var user = new AppUser()
            {
                UserName = registroVM.UserName,
                Cpf = registroVM.Cpf,
                Email = registroVM.Email,
                PhoneNumber = registroVM.Telefone
            };

            var result = await _userManager.CreateAsync(user, registroVM.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");
                await _signInManager.SignInAsync(user, isPersistent: false);

                return true;
            }

            return false;
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
