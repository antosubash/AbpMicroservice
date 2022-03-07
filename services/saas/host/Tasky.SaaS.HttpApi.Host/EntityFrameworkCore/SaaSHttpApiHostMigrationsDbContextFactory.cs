using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.SaaS.EntityFrameworkCore;

public class SaaSHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SaaSHttpApiHostMigrationsDbContext>
{
    public SaaSHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SaaSHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("SaaS"));

        return new SaaSHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
