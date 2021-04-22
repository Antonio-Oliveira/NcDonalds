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
        public int CategoriaId { get; set; }

        [StringLength(200)]
        public string Nome { get; set; }

        [StringLength(300)]
        public string Descricao { get; set; }

        //Relacionamento N : 1
        public List<Lanche> Lanches { get; set;}
    }
}
