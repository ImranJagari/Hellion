using Hellion.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Hellion.Core.Database
{
    public class DatabaseContext : DbContext
    {
        private DatabaseConfiguration configuration;

        public DatabaseContext(DatabaseConfiguration configuration)
            : base()
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = string.Format("server={0};userid={1};pwd={2};port=3306;database={3};sslmode=none;", 
                this.configuration.Ip, 
                this.configuration.User, 
                this.configuration.Password, 
                this.configuration.DatabaseName);

            optionsBuilder.UseMySql(connectionString);
        }

        public DbSet<DbUser> Users { get; set; }
    }
}
