using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ourTime_server.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ourTime_server.Services;   

namespace ourTime_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("name")]
        [Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            return Ok(_userService.Register(request));
        }

        [HttpPost("login")]
        public ActionResult<String> Login(UserDto request)
        {
            var token = _userService.Login(request);
            if(token == "Error")
            {
                return BadRequest("Username is not found or Password is wrong!");
            }
            return Ok(token);
        }

        /*private string CreateToken(User user)
        {
            return _userService.CreateToken(user);
        }*/
    }
}
