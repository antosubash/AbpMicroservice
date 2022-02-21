using Volo.Abp.Modularity;

namespace Tasky.Identity;

[DependsOn(
    typeof(IdentityApplicationModule),
    typeof(IdentityDomainTestModule)
    )]
public class IdentityApplicationTestModule : AbpModule
{

}
