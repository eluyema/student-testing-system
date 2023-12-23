using Microsoft.EntityFrameworkCore;

namespace student_testing_system.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration _config { get; set; }

        public AppDbContext(IConfiguration config) {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<User> Users { get; set; }
    }
}
