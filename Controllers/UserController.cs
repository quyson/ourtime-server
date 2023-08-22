using ourTime_server.Models;
using System.Threading.Tasks;
using ourTime_server.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace ourTime_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("testing")]
        public void /*Task<IActionResult>*/ Testing(string user)
        {
            Console.WriteLine(user);
        }

        [HttpGet("yo")]
        public string Yo()
        {
            return("Hello");
        }
    }
}


