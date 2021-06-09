using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Categoria
    {
        //Chave Primaria
        [Display(Name = "Id de categoria: ")]
        public int CategoriaId { get; set; }

        [Display(Name = "Nome: ")]
        [StringLength(200)]
        public string Nome { get; set; }

        [Display(Name = "Descrição: ")]
        [StringLength(300)]
        public string Descricao { get; set; }

        //Relacionamento N : 1
        public List<Lanche> Lanches { get; set;}
    }
}
