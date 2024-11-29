using Volo.Abp.Modularity;

namespace Tasky.WebApp;

[DependsOn(typeof(WebAppApplicationModule))]
[DependsOn(typeof(WebAppDomainTestModule))]
public class WebAppApplicationTestModule : AbpModule
{

}
