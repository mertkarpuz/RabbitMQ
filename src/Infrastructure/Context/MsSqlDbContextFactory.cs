using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Context
{
    public class MsSqlDbContextFactory : IDesignTimeDbContextFactory<MsSqlDbContext>
    {
        public IConfiguration configuration;
        public MsSqlDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MsSqlDbContext>();
            var folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(folderPath, "sharedsettings.json"), false, true)
                .AddEnvironmentVariables()
                .Build();
            optionsBuilder.UseSqlServer(GetDbConnectionString());
            return new MsSqlDbContext(optionsBuilder.Options);
        }
        private string GetDbConnectionString()
        {
            return configuration.GetSection("ConnectionStrings:MsSqlConnection").Value;
        }
    }
}
