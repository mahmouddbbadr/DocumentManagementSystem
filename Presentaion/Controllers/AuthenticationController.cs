using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
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
                if (result.Success) 
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if(ModelState.IsValid)
            {
                var result  =await userService.UserLogin(userLoginDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return Unauthorized(result);

            }
            return Unauthorized(ModelState);
        }


        [Authorize]
        [HttpGet("UserInformation")]
        public async Task<IActionResult> LoginUserInformation()
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginedUserInformation();
                if (result.Success)
                {
                    return Ok(result);
                }
                return Unauthorized(result);

            }
            return Unauthorized(ModelState);
        }

    }
}
