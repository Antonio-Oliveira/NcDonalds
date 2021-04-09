using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Data
{
    public static class SeedData
    {

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            // incluir perfis customizados
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            // definição das roles
            string[] rolesNames = { "Admin", "Member" };
            IdentityResult RoleResult;

            // criação das roles caso elas não existam
            foreach(var roleName in rolesNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if(!roleExist)
                {
                    RoleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Definições do PowerUser
            var powerUser = new AppUser()
            {
                UserName = configuration.GetSection("UserSettings")["UserName"],
                Email = configuration.GetSection("UserSettings")["UserEmail"],
            };

            var userPassword = configuration.GetSection("UserSettings")["UserPassword"];

            // Criar PowerUser caso seu email não exista
            var user = await UserManager.FindByEmailAsync(powerUser.Email);

            if (user == null)
            {
                // Cria o super usuário com os dados informados
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // Atribui o usuário ao perfil Admin
                    await UserManager.AddToRoleAsync(powerUser, "Admin");
                }
            }
        }
    }
}
