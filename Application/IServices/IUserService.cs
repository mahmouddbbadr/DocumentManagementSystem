using Application.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        public Task<(bool Success, string Message)> UserRegister(UserRegisterDto registerDto);
        public Task<(bool Success, string Token, string Message)> UserLogin(UserLoginDto loginDto);
        public Task<(bool Success, string Message)> BlockUser(string email);
        public Task<(bool Success, string Message)> UnBlockUser(string email);
        public Task<(bool Success, UserDto user, string Message)> GetUser(string email);
        public Task<(bool Success, ICollection<UserDto> users, string Message)> GetUnBlockedUsers();
        public Task<(bool Success, ICollection<UserDto> users, string Message)> GetBlockedUsers();






    }
}
