using Application.Dtos;
using Application.IRepository;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Infrasturcture.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Infrasturcture.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IGenericRepository<WorkSpace> workSpaceRepository;
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public UserService(UserManager<AppUser> userManager, IGenericRepository<WorkSpace> workSpaceRepository,
            IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.workSpaceRepository = workSpaceRepository;
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<(bool Success, string Message)> UserRegister(UserRegisterDto registerDto)
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
                        UserName = registerDto.Email,
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
                        
                        //await userManager.UpdateAsync(newUser);
                        await userManager.AddToRoleAsync(newUser, "User");
                        return (true, "Account added successfully" );
                    }
                    return (false, "Something went wrong while adding user, please enter valid data");
                }
                return (false, $"WorkSpace name \"{registerDto.WorkSpaceName}\" already exists");
                
            }
            return (false, "User already exists");
        }

        public async Task<(bool Success, string Token, string Message)> UserLogin(UserLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user != null && user.IsLocked == false) 
            {
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

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
                    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(3),
                        signingCredentials: signingCredentials,
                        claims: claims
                        );
                    return (true, new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), null);

                }
                return (false, null, "Password is not correct");

            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, UserDto user, string Message)> GetUser(string email)
        {
            var user = mapper.Map<UserDto>(userRepository.GetUserByEmail(email));
            if (user == null)
                return (false, null, $"No user was found with email \"{email}\"");
            return (true, user, "founfd");
        }

        public async Task<(bool Success, ICollection<UserDto> users, string Message)> GetUnBlockedUsers()
        {
            var users = mapper.Map<ICollection<UserDto>>(userRepository.GetUnBlockedUsers());
            if (users.Count == 0) 
                return(false, null, "No users was found");
            return (true, users, "");

        }

        public async Task<(bool Success, ICollection<UserDto> users, string Message)> GetBlockedUsers()
        {
            var users = mapper.Map<ICollection<UserDto>>(userRepository.GetBlockedUsers());
            if (users.Count == 0)
                return (false, null, "No users was found");
            return (true, users, "");
        }

        public async Task<(bool Success, string Message)> BlockUser(string email)
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null)
            {
                user.IsLocked = true;
                await userManager.UpdateAsync(user);
                return (true, "user has been blocked successfully");
            }
            return (false, "Can not find user");
        }

        public async Task<(bool Success, string Message)> UnBlockUser(string email)
        {
            var user = userRepository.GetBlockedUserByEmail(email);
            if (user != null)
            {
                user.IsLocked = false;
                await userManager.UpdateAsync(user);
                return (true, "user has been Unblocked successfully");
            }
            return (false, "Can not find user");
        }


    }
}
