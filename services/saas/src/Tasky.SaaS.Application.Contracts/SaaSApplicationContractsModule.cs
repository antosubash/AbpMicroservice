using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tasky.SaaS;

[DependsOn(
    typeof(SaaSDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class SaaSApplicationContractsModule : AbpModule
{

}
