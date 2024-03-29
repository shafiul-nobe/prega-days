using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PregaDays.Models.DTO;
using PregaDays.Repositories;

namespace PregaDays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository tokenRepository;
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var result = await userManager.FindByEmailAsync(identityUser.Email);
            if(result != null) 
            {
                return BadRequest("User is already registered");
            }
            var res = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(res.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) 
                {
                    res = await userManager.AddToRolesAsync(identityUser,registerRequestDto.Roles);
                    if(res.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
                var check = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (check)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user,roles.ToList());
                        return Ok(new LoginResponseDto { JwtToken = jwtToken });
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }


            
    }
}
