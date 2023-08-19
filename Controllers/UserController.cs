using ourTime_server.Models;
using System.Threading.Tasks;
using ourTime_server.DBContext;

namespace ourTime_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

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

            return (model);
            /*var newUser = new User
            {
                Username = model.Username,
                Email = model.Email,
                // You should hash and salt the password before storing it
                Password = HashAndSaltPassword(model.Password),
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Registration successful" });*/
        }

        [HttpPost("login")]
        public IActionResult GetUserProfile(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            // Return user profile data
            return Ok(user);
        }

        // Hash and salt password using a secure hashing algorithm (e.g., bcrypt)
        private string HashAndSaltPassword(string password)
        {
            // Implement your password hashing logic here
        }
    }
}


