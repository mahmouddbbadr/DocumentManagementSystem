using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthenticationController(IUserService userService)
        {
            this.userService = userService;
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if(ModelState.IsValid)
            {
                var result = await userService.UserRegister(userRegisterDto);
                if (result.Success == true) 
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);

            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if(ModelState.IsValid)
            {
                var result  =await userService.UserLogin(userLoginDto);
                if (result.Success == true)
                {
                    return Ok(new {result.Success, result.Token, result.Message });
                }
                return Unauthorized(result.Message);

            }
            return Unauthorized(ModelState);
        }

    }
}
