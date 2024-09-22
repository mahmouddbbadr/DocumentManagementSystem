using Application.Dtos;
using Application.IRepository;
using Application.Services;
using AutoMapper;
using DocumentManagementSystem.Services.Dtos;
using DocumentManagementSystem.Services.ResultPattern;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Infrasturcture.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IGenericRepository<WorkSpace> workSpaceRepository;
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IGenericRepository<WorkSpace> workSpaceRepository,
            IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.workSpaceRepository = workSpaceRepository;
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<GenericResult> UserRegister(UserRegisterDto registerDto)
        {   
            
            var checkUserExists = await userManager.FindByEmailAsync(registerDto.Email);
            if(checkUserExists == null)
            {
                var checkWorkSpaceExists = workSpaceRepository.CheckEntityExits(registerDto.WorkSpaceName);
                if(checkWorkSpaceExists != true)
                {
                    var newUser = new AppUser()
                    {
                        Email = registerDto.Email,
                        UserName = registerDto.Email.Substring(0, registerDto.Email.IndexOf('@')),
                        Address = registerDto.Address,
                        NId = registerDto.NId,
                        PhoneNumber = registerDto.PhoneNumber
                    };
                    var workSpace = new WorkSpace()
                    {
                        Name = registerDto.WorkSpaceName,
                    };
                    workSpaceRepository.Create(workSpace);
                    newUser.WorkspaceId = workSpace.Id;

                    var isCreated = await userManager.CreateAsync(newUser, registerDto.Password);
                    if(isCreated.Succeeded) 
                    {
                        await userManager.AddToRoleAsync(newUser, "User");
                        return (new GenericResult() { Success = true, Body = null, Message = "Account added successfully" });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "Something went wrong while adding user, please try enter valid data" });

                }
                return (new GenericResult() { Success = false, Body = null, Message = $"WorkSpace name \"{registerDto.WorkSpaceName}\" already exists" });


            }
            return (new GenericResult() { Success = false, Body = null, Message = "User already exists" });

        }

        public async Task<GenericResult> UserLogin(UserLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && user.IsLocked == false) 
            {
                var workwpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, user.Id);
                user.WorkSpace = workwpace;
                var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if(checkPassword)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    string userRole = roles.FirstOrDefault();

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
                    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                        (
                        expires: DateTime.Now.AddDays(3),
                        signingCredentials: signingCredentials,
                        claims: claims
                        );
                    var mappedUser = mapper.Map<UserOutputDto>(user);
                    return (new GenericResult(){
                        Success = true,
                        Body = new {Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), Role = userRole, User = mappedUser },
                        Message = null 
                    });


                }
                return (new GenericResult() { Success = false, Body = null, Message = "Password is not correct" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> GetUser(string email)
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null)
            {
            var workwpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, user.Id);
            user.WorkSpace = workwpace;
            var mappedUser = mapper.Map<UserOutputDto>(user);
                return (new GenericResult() { Success = true, Body = mappedUser, Message = null });

            }
            return (new GenericResult() { Success = false, Body = null, Message = $"No user was found with email \"{email}\"" });
        }

        public async Task<GenericResult> GetUnBlockedUsers()
        {
            var users = (userRepository.GetUnBlockedUsers());
            if (users.Count != 0)
            {
                List<UserOutputDto> mappedUsers = new List<UserOutputDto>();
                foreach (var user in users)
                {
                    var workwpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, user.Id);
                    user.WorkSpace = workwpace;
                    var mappedUser = mapper.Map<UserOutputDto>(user);
                    mappedUsers.Add(mappedUser);
                }
                return (new GenericResult() { Success = true, Body = mappedUsers, Message = null });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "No users was found" });
        }

        public async Task<GenericResult> GetBlockedUsers()
        {
            var users = (userRepository.GetBlockedUsers());
            if (users.Count != 0)
            {
                List<UserOutputDto> mappedUsers = new List<UserOutputDto>();
                foreach (var user in users)
                {
                    var workwpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, user.Id);
                    user.WorkSpace = workwpace;
                    var mappedUser = mapper.Map<UserOutputDto>(user);
                    mappedUsers.Add(mappedUser);
                }
                return (new GenericResult() { Success = true, Body = mappedUsers, Message = null});

            }
            return (new GenericResult() { Success = false, Body = null, Message = "No users was found" });
        }

        public async Task<GenericResult> BlockUser(string email)
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null)
            {
                user.IsLocked = true;
                await userManager.UpdateAsync(user);
                return (new GenericResult() { Success = true, Body = null, Message = "user has been blocked successfully" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "Can not find user" });
        }

        public async Task<GenericResult> UnBlockUser(string email)
        {
            var user = userRepository.GetBlockedUserByEmail(email);
            if (user != null)
            {
                user.IsLocked = false;
                await userManager.UpdateAsync(user);
                return (new GenericResult() { Success = true, Body = null, Message = "user has been Unblocked successfully" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "Can not find user" });
        }

        public async Task<GenericResult> LoginedUserInformation()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                var workSpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, userId);
                user.WorkSpace = workSpace; 
                var mappedUser = mapper.Map<UserOutputDto>(user);
                return (new GenericResult() { Success = true, Body = mappedUser, Message = null});


            }
            return (new GenericResult() { Success = false, Body = null, Message = "Can not find user" });
        }
        public async Task<GenericResult> AdminGetUserInformation( string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user != null)
            {
            var workSpace = workSpaceRepository.GetByIdOrName(user.WorkspaceId, userId);
            user.WorkSpace = workSpace;
            var mappedUser = mapper.Map<UserOutputDto>(user);
            return (new GenericResult() { Success = true, Body = mappedUser, Message = null });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "Can not find user" });
        }

    }
}
