using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AdministrationDomainSharedModule)
)]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
    [DependsOn(typeof(AbpSettingManagementDomainModule))]
    public class AdministrationDomainModule : AbpModule
{

}
