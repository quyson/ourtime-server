using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ourTime_server.Models;

namespace ourTime_server.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
    }
}
