using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        public string UserId { get; set; }

        public string Detalhe { get; set; }

        public string Cep { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; }

    }
}
