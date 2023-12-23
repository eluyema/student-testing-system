using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using student_testing_system.Models.Users;

namespace student_testing_system.Data
{
    public class AppDbContext : IdentityDbContext<User>
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
