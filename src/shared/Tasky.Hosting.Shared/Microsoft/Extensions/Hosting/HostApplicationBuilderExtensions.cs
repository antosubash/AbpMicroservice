using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddSharedEndpoints(this IHostApplicationBuilder builder)
    {
        builder.AddRabbitMQClient(connectionName: TaskyNames.RabbitMq, action => action.ConnectionString = builder.Configuration.GetConnectionString(TaskyNames.RabbitMq));
        builder.AddRedisDistributedCache(connectionName: TaskyNames.Redis);
        builder.AddSeqEndpoint(connectionName: TaskyNames.Seq);

        return builder;
    }
}
