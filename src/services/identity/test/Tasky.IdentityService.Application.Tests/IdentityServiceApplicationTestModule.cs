using Volo.Abp.Modularity;

namespace Tasky.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceDomainTestModule))]
public class IdentityServiceApplicationTestModule : AbpModule
{

}
