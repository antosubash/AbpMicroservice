using Serilog;
using Tasky.Administration.EntityFrameworkCore;
using Tasky.Projects.EntityFrameworkCore;
using Tasky.SaaS.EntityFrameworkCore;
using Tasky.WebApp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tasky.DbMigrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
            .WriteTo.Async(c => c.Console())
#else
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
#endif
            .Enrich.FromLogContext()
            .CreateLogger();

        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddNpgsqlDbContext<AdministrationDbContext>(connectionName: TaskyNames.AdministrationDb);
        builder.AddNpgsqlDbContext<IdentityDbContext>(connectionName: TaskyNames.IdentityServiceDb);
        builder.AddNpgsqlDbContext<SaaSDbContext>(connectionName: TaskyNames.SaaSDb);
        builder.AddNpgsqlDbContext<ProjectsDbContext>(connectionName: TaskyNames.ProjectsDb);
        builder.AddNpgsqlDbContext<WebAppDbContext>(connectionName: TaskyNames.WebAppDb);

        builder.Configuration.AddAppSettingsSecretsJson();

        builder.Logging.AddSerilog();

        builder.Services.AddHostedService<DbMigratorHostedService>();

        var host = builder.Build();

        await host.RunAsync();
    }
}
