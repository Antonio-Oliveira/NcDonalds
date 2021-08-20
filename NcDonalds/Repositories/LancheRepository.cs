using NcDonalds.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NcDonalds.Context;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NcDonalds.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Lanche>> GetLanches() => Task.FromResult(_context.Lanches.Include(c => c.Categoria).ToList());

        public Task<Lanche> GetLancheById(int lancheId) => Task.FromResult(_context.Lanches.FirstOrDefault(l => l.LancheId == lancheId));

        public async Task<bool> AddLanche(Lanche lanche)
        {
            if (lanche != null)
            {
                _context.Lanches.Add(lanche);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> EmEstoque(int lancheId)
        {
            var lanche = await _context.Lanches.FindAsync(lancheId);

            if (lanche != null)
            {
                lanche.EmEstoque = !lanche.EmEstoque;
                _context.Lanches.Update(lanche);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateLanche(Lanche lanche)
        {

            if (lanche.LancheId != 0)
            {
                _context.Update(lanche);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
