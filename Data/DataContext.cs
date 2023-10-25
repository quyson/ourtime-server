using Microsoft.EntityFrameworkCore;
using ourTime_server.Models;
using System.Data;

namespace ourTime_server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
