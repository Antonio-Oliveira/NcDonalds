using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetCategorias();

        Categoria GetCategoriaById(int categoriaId);

        Task<Categoria> GetCategoriaByName(string categoria);

        Task<bool> AddCategoria(Categoria categoria);

        Task<bool> RemoveCategoria(int categoriaId);

        Task<bool> UpdateCategoria(Categoria categoria);
    }
}
