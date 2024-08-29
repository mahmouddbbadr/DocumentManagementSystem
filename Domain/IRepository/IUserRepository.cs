using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IUserRepository 
    {
        AppUser GetUserByEmail(string email);
        AppUser GetBlockedUserByEmail(string email);
        ICollection<AppUser> GetUnBlockedUsers();
        ICollection<AppUser> GetBlockedUsers();

    }
}
