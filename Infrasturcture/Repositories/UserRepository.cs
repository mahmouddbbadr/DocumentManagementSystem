using Application.IRepository;
using Domain.Models;
using Infrasturcture.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }


        public ICollection<AppUser> GetUnBlockedUsers()
        {
            return userManager.Users.Where(u=> u.IsLocked == false).ToList();
        }

        public ICollection<AppUser> GetBlockedUsers()
        {
            return userManager.Users.Where(u=> u.IsLocked == true).ToList();
        }

        public AppUser GetUserByEmail(string email)
        {
            return userManager.Users.Where(u => u.IsLocked == false).FirstOrDefault(e => e.Email.Trim().ToLower() == email.Trim().ToLower());
        }

        public AppUser GetBlockedUserByEmail(string email)
        {
            return userManager.Users.Where(u => u.IsLocked == true).FirstOrDefault(e => e.Email.Trim().ToLower() == email.Trim().ToLower());
        }

    }
}
