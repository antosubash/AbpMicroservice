using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class AdministrationApplicationContractsModule : AbpModule
{

}
