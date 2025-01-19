using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tasky.Administration.HttpApi.Client.ConsoleTestApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .AddAppSettingsSecretsJson()
            .ConfigureServices(
                (hostContext, services) =>
                {
                    services.AddHostedService<ConsoleTestAppHostedService>();
                }
            );
    }
}
