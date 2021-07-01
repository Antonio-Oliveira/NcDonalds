using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class EnderecoViewModel
    {
        public int EnderecoId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Detalhe")]
        public string Detalhe { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Display(Name = "Número")]
        [Range(0, int.MaxValue)]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        public string Cep { get; set; }
    }
}

