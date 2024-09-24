using Application.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;




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

        public ICollection<AppUser> SearchUnBlocked(string filter)
        {
            return userManager.Users.Where(u => u.IsLocked == false && u.Email.ToLower().StartsWith(filter.ToLower())).ToList();
        }
        public ICollection<AppUser> SearchBlocked(string filter)
        {
            return userManager.Users.Where(u => u.IsLocked == true && u.Email.ToLower().StartsWith(filter.ToLower())).ToList();
        }

    }
}
