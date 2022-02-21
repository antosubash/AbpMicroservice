using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.SaaS;

[DependsOn(
    typeof(SaaSApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class SaaSHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SaaSApplicationContractsModule).Assembly,
            SaaSRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SaaSHttpApiClientModule>();
        });

    }
}
