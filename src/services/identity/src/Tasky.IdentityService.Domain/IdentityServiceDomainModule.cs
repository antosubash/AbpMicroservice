using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;

namespace Tasky.IdentityService;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(IdentityServiceDomainSharedModule))]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpOpenIddictDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainOpenIddictModule))]
public class IdentityServiceDomainModule : AbpModule { }
