using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.PermissionManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
[DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
    public class AdministrationApplicationContractsModule : AbpModule
{

}
