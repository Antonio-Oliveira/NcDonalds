using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NcDonalds.Models;
using System;
using System.Threading.Tasks;

namespace NcDonalds.Data
{
    public static class SeedData
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            // Incluir perfis costumizados
            var RoleManager = serviceProvider.GetRequiredService< RoleManager<IdentityRole> >();
            var UserManager = serviceProvider.GetRequiredService< UserManager<AppUser> >();

            // Define os perfis em um array de strings
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            // Percorre o array de strings
            // Verifica se o perfil ja existe
            foreach (var roleName in roleNames)
            {
                // Cria perfis e os inclui no banco de dados
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Cria um super usuário que pode manter a aplicação web
            var poweruser = new AppUser
            {
                UserName = configuration.GetSection("UserSettings")["UserName"],
                Email = configuration.GetSection("UserSettings")["UserEmail"]
            };

            // Obtem a senha do arquivo de configuração
            string userPassword = configuration.GetSection("UserSettings")["UserPassword"];

            // Verifica se existe um usuário com o email informado
            var user = await UserManager.FindByEmailAsync(configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null)
            {
                // Cria o super usuário com os dados informados
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // Atribui o usuário ao perfil Admin
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
