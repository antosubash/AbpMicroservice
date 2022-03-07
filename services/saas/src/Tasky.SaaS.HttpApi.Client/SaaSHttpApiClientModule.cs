using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.TenantManagement;

namespace Tasky.SaaS;

[DependsOn(
    typeof(SaaSApplicationContractsModule),
    typeof(AbpHttpClientModule))]
[DependsOn(typeof(AbpTenantManagementHttpApiClientModule))]
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
