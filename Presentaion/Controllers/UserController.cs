using Application.Dtos;
using Application.IRepository;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Presentaion.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;



        public UserController(IUserService userService)
        {
            this.userService = userService;

        }

        [HttpGet("Email")]
        public async Task<IActionResult> GetUser(string email)
        {
            var result = await userService.GetUser(email);
            if(result.Success)
            {
                return Ok(result.user);
            }
            return NotFound(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnBlockedUsers()
        {
            var result = await userService.GetUnBlockedUsers();
            if (result.Success)
                return Ok(result.users);
            return NotFound(result.Message);
        }

        [HttpGet("BlockedUsers")]
        public async Task<IActionResult> GetBlockedUsers()
        {
            var result = await userService.GetBlockedUsers();
            if (result.Success)
                return Ok(result.users);
            return NotFound(result.Message);
        }

        [HttpPut("Block")]
        public async Task<IActionResult> BlockUser(string email)
        {
            var result = await userService.BlockUser(email);
            if (result.Success)
                return Ok(result.Message);
            return NotFound(result.Message);
        }

        [HttpPut("UnBlock")]
        public async Task<IActionResult> UnBlockUser(string email)
        {
            var result = await userService.UnBlockUser(email);
            if (result.Success)
                return Ok(result.Message);
            return NotFound(result.Message);
        }

    }
}
