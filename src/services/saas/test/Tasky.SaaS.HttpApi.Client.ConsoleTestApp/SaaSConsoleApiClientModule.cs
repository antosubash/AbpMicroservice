using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.SaaS.HttpApi.Client.ConsoleTestApp;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(SaaSHttpApiClientModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelModule))]
public class SaaSConsoleApiClientModule : AbpModule
{

}
