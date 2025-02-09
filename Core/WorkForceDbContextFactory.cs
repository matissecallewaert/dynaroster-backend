using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Core
{
    public class WorkForceDbContextFactory : IDesignTimeDbContextFactory<WorkForceDbContext>
    {
        public WorkForceDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory(); // Use current directory

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true) // Add for dev environment
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<WorkForceDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new WorkForceDbContext(optionsBuilder.Options);
        }
    }
}