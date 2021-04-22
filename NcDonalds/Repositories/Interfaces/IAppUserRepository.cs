using NcDonalds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        public AppUser GetUser(string userName);
        public AppUser GetUserById(string id);

    }
}
