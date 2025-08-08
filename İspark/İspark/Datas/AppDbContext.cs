using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using İspark.Model;

namespace İspark.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<News> News { get; set; }
    }
}