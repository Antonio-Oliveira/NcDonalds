using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public AppUser GetUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == userName);
            return user;
        }

        public AppUser GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == id);
            return user;
        }
    }
}
