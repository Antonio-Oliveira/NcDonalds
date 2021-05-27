using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Models
{
    public class UserCupom
    {
        public int UserCupomId { get; set; }

        public int UserId { get; set; }

        public int CupomId { get; set; }

    }
}
