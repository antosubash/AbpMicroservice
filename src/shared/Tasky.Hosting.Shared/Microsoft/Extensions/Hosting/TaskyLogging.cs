using Serilog;
using Serilog.Events;

namespace Microsoft.Extensions.Hosting
{
    public static class TaskyLogging
    {
        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
                .WriteTo.Async(c => c.Console())
#else
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
