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
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var result = await _userService.Register(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await _userService.Login(request);
            if(token == "Error")
            {
                return BadRequest("Username is not found or Password is wrong!");
            }
            if(token == "User not found!")
            {
                return BadRequest("User not found!");
            }
            return Ok(token);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {

            var result = await _userService.DeleteUser(id);

            if(result == "User not found!")
            {
                return BadRequest("User not found!");
            }
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateUser(UserDto request)
        {

            var result = await _userService.UpdateUser(request);

            if (result == "User not found!")
            {
                return BadRequest("User not found!");
            }
            return Ok(result);
        }
    }
}
