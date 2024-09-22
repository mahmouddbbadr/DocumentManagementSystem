using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetUserIngormation(string id)
        {
            var result = await userService.AdminGetUserInformation(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnBlockedUsers()
        {
            var result = await userService.GetUnBlockedUsers();
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        [HttpGet("BlockedUsers")]
        public async Task<IActionResult> GetBlockedUsers()
        {
            var result = await userService.GetBlockedUsers();
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPut("Block")]
        public async Task<IActionResult> BlockUser(string email)
        {
            var result = await userService.BlockUser(email);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPut("UnBlock")]
        public async Task<IActionResult> UnBlockUser(string email)
        {
            var result = await userService.UnBlockUser(email);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

    }
}
