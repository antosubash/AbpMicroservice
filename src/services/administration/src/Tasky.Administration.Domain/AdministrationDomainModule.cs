using Volo.Abp.AuditLogging;
using Volo.Abp.Domain;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(AdministrationDomainSharedModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpPermissionManagementDomainOpenIddictModule))]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
[DependsOn(typeof(AbpAuditLoggingDomainModule))]
[DependsOn(typeof(AbpFeatureManagementDomainModule))]
public class AdministrationDomainModule : AbpModule
{
}
