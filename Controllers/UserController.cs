using ourTime_server.Models;
using System.Threading.Tasks;
using ourTime_server.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace ourTime_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public void /*Task<IActionResult>*/ Testing()
        {
            Console.WriteLine("what the hell bro!");
        }

        [HttpGet]
        [Route("yo")]
        public string Yo()
        {
            return("Hello");
        }
    }
}


