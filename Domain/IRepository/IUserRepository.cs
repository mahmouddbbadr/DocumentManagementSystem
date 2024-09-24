using Domain.Models;




namespace Application.IRepository
{
    public interface IUserRepository 
    {
        AppUser GetUserByEmail(string email);
        AppUser GetBlockedUserByEmail(string email);
        ICollection<AppUser> GetUnBlockedUsers();
        ICollection<AppUser> GetBlockedUsers();
        ICollection<AppUser> SearchUnBlocked(string filter);
        ICollection<AppUser> SearchBlocked(string filter);


    }
}
