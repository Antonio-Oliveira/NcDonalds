using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Endereco> GetEnderecos(string userId) => _context.Enderecos.Where(e => e.UserId == userId).ToList();


        public Endereco GetEnderecoById(string userId, int enderecoId) =>
            _context.Enderecos.FirstOrDefault(e => e.EnderecoId == enderecoId && e.UserId == userId);

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
                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {

            var result = await _context.Enderecos.FindAsync(endereco.EnderecoId);

            if (result != null)
            {
                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


    }
}
