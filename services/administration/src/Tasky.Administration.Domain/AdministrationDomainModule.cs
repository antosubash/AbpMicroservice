using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AdministrationDomainSharedModule),
    typeof(AbpPermissionManagementDomainIdentityServerModule),
    typeof(AbpPermissionManagementDomainIdentityModule)
)]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
[DependsOn(typeof(AbpAuditLoggingDomainModule))]
[DependsOn(typeof(AbpFeatureManagementDomainModule))]
public class AdministrationDomainModule : AbpModule
{
}