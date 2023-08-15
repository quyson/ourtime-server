using ourTime_server.Models;
using System.Threading.Tasks;

namespace ourTime_server.Controllers
{
    public class UserController : Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly ApplicationDbContext _context; // Replace with your DbContext

            public UserController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegistrationModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newUser = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    // You should hash and salt the password before storing it
                    Password = HashAndSaltPassword(model.Password),
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Registration successful" });
            }

            // Hash and salt password using a secure hashing algorithm (e.g., bcrypt)
            private string HashAndSaltPassword(string password)
            {
                // Implement your password hashing logic here
            }
        }
    }
}

   
