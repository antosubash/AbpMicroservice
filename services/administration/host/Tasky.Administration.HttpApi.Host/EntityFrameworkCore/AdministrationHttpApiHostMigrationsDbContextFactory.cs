using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.Administration.EntityFrameworkCore;

public class AdministrationHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AdministrationHttpApiHostMigrationsDbContext>
{
    public AdministrationHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AdministrationHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("AdministrationService"));

        return new AdministrationHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
