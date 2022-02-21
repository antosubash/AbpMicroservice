using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.Identity.EntityFrameworkCore;

public class IdentityHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityHttpApiHostMigrationsDbContext>
{
    public IdentityHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Identity"));

        return new IdentityHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
