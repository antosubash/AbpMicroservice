using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.AuditLogging;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AdministrationDomainSharedModule)
)]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
    [DependsOn(typeof(AbpSettingManagementDomainModule))]
    [DependsOn(typeof(AbpAuditLoggingDomainModule))]
    public class AdministrationDomainModule : AbpModule
{

}
