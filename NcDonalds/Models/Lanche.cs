using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Preço: R$ ")]
        public int Preco { get; set; }

        [Display(Name = "Descrição curta: ")]
        [StringLength(200)]
        public string DescricaoCurta { get; set; }

        [Display(Name = "Descrição detalhada: ")]
        [StringLength(200)]
        public string DescricaoDetalhada { get; set; }

        public string ImagemURL { get; set; }

        [Display(Name = "Em estoque: ")]
        public bool EmEstoque { get; set; }

        //Chave estrangeira
        [Display(Name = "Id de Categoria: ")]
        public int CategoriaId { get; set; }
        
        // Relacionamento 1 : N
        public virtual Categoria Categoria { get; set; }

    }
}
