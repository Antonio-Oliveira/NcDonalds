using NcDonalds.Models;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        AppUser GetUser(string userName);

        AppUser GetUserById(string id);

        Task<bool> Login(LoginViewModel loginVM);

        Task<bool> Register(RegisterViewModel registroVM);

        void Logout();

        Endereco GetEnderecoUserById(string userId, int enderecoId);

        IEnumerable<Endereco> GetEnderecosByUserId(string userId);

        Endereco GetEnderecosById(int enderecoId);

        Task<bool> AddEndereco(Endereco endereco);

        Task<bool> RemoveEndereco(int enderecoId);

        Task<bool> UpdateEndereco(Endereco endereco);

    }
}
