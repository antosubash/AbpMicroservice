using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.Administration.HttpApi.Client.ConsoleTestApp;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AdministrationHttpApiClientModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelModule))]
public class AdministrationConsoleApiClientModule : AbpModule { }
