using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string Cpf { get; set; }
    }
}
