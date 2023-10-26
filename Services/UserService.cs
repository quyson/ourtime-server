using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ourTime_server.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ourTime_server.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.SignalR;
using ourTime_server.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ourTime_server.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public string GetMyName()
        {
            var result = String.Empty;
            if(_HttpContextAccessor.HttpContext != null)
            {
                result = _HttpContextAccessor.HttpContext.User?.Identity?.Name;
          
            }
            return result;
        }

        public async Task<ActionResult<string>> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User();

            user.Username = request.Username;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PasswordHash = passwordHash;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Console.WriteLine(_context.Database.GetDbConnection().ConnectionString);

            return ("User Created!");
        }

        public async Task<string> Login(UserDto request) 
        {
            var user = await _context.Users.Where(b => b.Username == request.Username).FirstOrDefaultAsync();
            Console.WriteLine(user);
            if(user == null)
            {
                return ("User not found!");
            }
            if (user.Username != request.Username || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)
            )
            {
                return("Error");
            };
            string token = CreateToken(user);
            return(token);
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
             );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
