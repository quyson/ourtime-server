using Microsoft.AspNetCore.Mvc;
using ourTime_server.Models;

namespace ourTime_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PasswordHash = passwordHash;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            if(user.Username != request.Username || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)
            {
                return BadRequest("Username is not found or Password is wrong!")
            }
            return Ok(user);
        }
    }
}
