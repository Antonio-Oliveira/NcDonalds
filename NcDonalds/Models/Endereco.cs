using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Escolha um detalhe para diferenciar seus endereços")]
        public string Detalhe { get; set; }

        [Required(ErrorMessage = "Informe seu CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe seu Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Informe sua Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe seu Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe sua Rua")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Informe o número identificador da sua residência")]
        [Range(0, int.MaxValue)]
        public int Numero { get; set; }

        public string Complemento { get; set; }

    }
}
