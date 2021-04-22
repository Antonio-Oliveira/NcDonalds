using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.ViewModel
{
    public class ProfileViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o seu nome")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Informe o email.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [Display(Name = "Telefone/Celular")]
        [Required(ErrorMessage = "Informe o seu Telefone/Celular")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "Cpf")]
        [Required(ErrorMessage = "Informe seu CPF")]
        [DataType(DataType.Text)]
        public string Cpf { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe uma Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
