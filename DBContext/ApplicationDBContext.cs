using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ourTime_server.Models;

namespace ourTime_server.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
