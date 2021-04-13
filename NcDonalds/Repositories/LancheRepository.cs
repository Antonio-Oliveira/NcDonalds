using Microsoft.EntityFrameworkCore;
using NcDonalds.Context;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include( c => c.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
           return  _context.Lanches.FirstOrDefault( l => l.LancheId == lancheId);
        }
            
    }
}
