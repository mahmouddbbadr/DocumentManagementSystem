using Application.Dtos;
using DocumentManagementSystem.Services.ResultPattern;


namespace Application.Services
{
    public interface IUserService
    {
        public Task<GenericResult> UserRegister(UserRegisterDto registerDto);
        public Task<GenericResult> UserLogin(UserLoginDto loginDto);
        public Task<GenericResult> LoginedUserInformation();
        public Task<GenericResult> AdminGetUserInformation(string userId);

        public Task<GenericResult> BlockUser(string email);
        public Task<GenericResult> UnBlockUser(string email);
        public Task<GenericResult> GetUser(string email);
        public Task<GenericResult> GetUnBlockedUsers();
        public Task<GenericResult> GetBlockedUsers();


    }
}
