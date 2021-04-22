using NcDonalds.Repositories.Interfaces;
using NcDonalds.Context;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;

        public Categoria GetCategoriaById(int categoriaId)
        {
            return _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoriaId);
        }

        public async Task<bool> AddCategoria(Categoria categoria)
        {
            if (categoria != null)
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveCategoria(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);

            if (categoria != null)
            {
                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCategoria(Categoria categoria)
        {
            if(categoria != null)
            {
                if (categoria.CategoriaId != 0)
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
