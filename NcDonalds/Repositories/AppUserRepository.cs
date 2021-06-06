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

        public AppUser GetUser(string userName) => _context.Users.FirstOrDefault(user => user.UserName == userName);

        public AppUser GetUserById(string id) => _context.Users.FirstOrDefault(user => user.Id == id);

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

        public IEnumerable<Endereco> GetEnderecosByUserId(string userId) => _context.Enderecos.Where(e => e.UserId == userId).ToList();

        public Endereco GetEnderecosById(int enderecoId) =>  _context.Enderecos.Find(enderecoId);

        public async Task<bool> AddEndereco(Endereco endereco)
        {
            if (endereco != null)
            {
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveEndereco(int enderecoId)
        {
            var endereco = await _context.Enderecos.FindAsync(enderecoId);

            if (endereco != null)
            {
                endereco.UserId = null;
                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            if(endereco.EnderecoId != 0)
            {
                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
