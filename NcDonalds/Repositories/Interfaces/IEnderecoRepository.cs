using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<bool> AddEndereco(Endereco endereco);

        Task<bool> RemoveEndereco(int enderecoId);

        Task<bool> UpdateEndereco(Endereco endereco);

        IEnumerable<Endereco> GetEnderecos(string userId);

        Endereco GetEnderecoById(string userId, int enderecoId);

    }
}
