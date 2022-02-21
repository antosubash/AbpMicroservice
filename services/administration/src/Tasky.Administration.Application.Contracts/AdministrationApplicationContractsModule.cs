using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
[DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
    [DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
    public class AdministrationApplicationContractsModule : AbpModule
{

}
