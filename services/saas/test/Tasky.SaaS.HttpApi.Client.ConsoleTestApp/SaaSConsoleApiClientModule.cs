using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.SaaS;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SaaSHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SaaSConsoleApiClientModule : AbpModule
{

}
