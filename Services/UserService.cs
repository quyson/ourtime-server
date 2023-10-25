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

namespace ourTime_server.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IConfiguration _configuration;
        public static User user = new User();

        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
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

        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PasswordHash = passwordHash;
            return user;
        }

        public string Login(UserDto request) 
        {
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
