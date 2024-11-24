using Volo.Abp.Modularity;

namespace Tasky.WebApp;

[DependsOn(typeof(WebAppDomainModule))]
[DependsOn(typeof(WebAppTestBaseModule))]
public class WebAppDomainTestModule : AbpModule
{

}
