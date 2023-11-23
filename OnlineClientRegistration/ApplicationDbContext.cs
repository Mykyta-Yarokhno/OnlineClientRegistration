using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;

namespace OnlineClientRegistration
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Record> Records => Set<Record>();
        public DbSet<TimeTable> TimeTables => Set<TimeTable>();
        public ApplicationDbContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=OnlineClientRegistration.db");
        }
    }
}
