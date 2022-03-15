using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;

namespace Tasky.IdentityService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityServiceDomainSharedModule)
)]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpIdentityServerDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityServerModule))]
public class IdentityServiceDomainModule : AbpModule
{
}