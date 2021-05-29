using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class CupomRepository : ICupomRepository
    {
        private AppDbContext _context;

        public CupomRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cupom> GetCupons => _context.Cupons.ToList();

        public Cupom GetCupomByName(string codigoCupom) => _context.Cupons.FirstOrDefault(c => c.Nome == codigoCupom);

        public async Task<bool> AddCupom(Cupom cupom)
        {
            if(cupom != null)
            {
                _context.Cupons.Add(cupom);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<bool> RemoveCupom(int cupomId)
        {
            var cupom = await _context.Cupons.FindAsync(cupomId);

            if(cupom != null)
            {
                _context.Cupons.Update(cupom);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCupom(Cupom cupom)
        {
            var result = await _context.Cupons.FindAsync(cupom);

            if(result != null)
            {
               _context.Cupons.Update(cupom);
               await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
