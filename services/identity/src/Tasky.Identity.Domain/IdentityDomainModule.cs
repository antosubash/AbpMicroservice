using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.Identity;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityDomainSharedModule)
)]
public class IdentityDomainModule : AbpModule
{

}
