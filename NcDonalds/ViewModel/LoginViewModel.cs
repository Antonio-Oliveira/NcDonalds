using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage ="Informe o seu nome")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage ="Informe uma Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
