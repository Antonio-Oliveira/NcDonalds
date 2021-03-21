using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Lanche
    {   
        //Chave Primaria
        public int LancheId { get; set; }

        [StringLength(200)]
        public string Nome { get; set; }

        public int Preco { get; set; }

        [StringLength(200)]
        public string DescricaoCurta { get; set; } 

        [StringLength(200)]
        public string DescricaoDetalhada { get; set; }

        public string ImagemURL { get; set; }

        public string ImagemThumbURL { get; set; }
        
        public bool EmEstoque { get; set; }

        //Chave estrangeira
        public int CategoriaId { get; set; }
        
        // Relacionamento 1 : N
        public virtual Categoria categoria { get; set; }

    }
}
