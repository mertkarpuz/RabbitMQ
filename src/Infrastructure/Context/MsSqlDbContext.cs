using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
