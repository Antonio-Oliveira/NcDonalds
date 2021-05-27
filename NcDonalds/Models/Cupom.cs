using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class Cupom
    {
        public int CupomId { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }

        public decimal Valor { get; set; }

        public decimal CompraMinima { get; set; }

        public decimal CompraMaxima { get; set; }

        public bool PrimeiraPedido { get; set; }

        [Display(Name = "Data/Hora da Emissão do Cupom")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Emissão { get; set; }

        [Display(Name = "Data/Hora do Vencimento do Cupom")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Vencimento { get; set; }
    }
}
