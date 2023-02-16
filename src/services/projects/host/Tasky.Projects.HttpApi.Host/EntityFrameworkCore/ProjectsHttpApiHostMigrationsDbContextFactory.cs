using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.Projects.EntityFrameworkCore;

public class
    ProjectsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ProjectsHttpApiHostMigrationsDbContext>
{
    public ProjectsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ProjectsHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Projects"));

        return new ProjectsHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}