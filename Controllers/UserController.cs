using Microsoft.AspNetCore.Mvc;

namespace ourTime_server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return ("Hello");
        }

        [HttpGet("LOL")]
        public string Lol()
        {
            return ("LOL");
        }
    }
}
