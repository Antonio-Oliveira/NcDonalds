using Microsoft.AspNetCore.Http;
using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class AdminLancheViewModel
    {
        public int LancheId { get; set; }

        public string Nome { get; set; }

        public int Preco { get; set; }
        
        public string DescricaoCurta { get; set; } 
       
        public string DescricaoDetalhada { get; set; }

        public virtual IFormFile Image { get; set; }

        public string ImagemURL { get; set; }

        public bool EmEstoque { get; set; }

        //Chave estrangeira
        public int CategoriaId { get; set; }
        
        // Relacionamento 1 : N
        public virtual Categoria Categoria { get; set; }


    }
}
