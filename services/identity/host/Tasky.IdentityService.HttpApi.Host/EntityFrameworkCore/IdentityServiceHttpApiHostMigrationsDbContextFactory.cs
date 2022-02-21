using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.IdentityService.EntityFrameworkCore;

public class IdentityServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceHttpApiHostMigrationsDbContext>
{
    public IdentityServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityServiceHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("IdentityService"));

        return new IdentityServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
