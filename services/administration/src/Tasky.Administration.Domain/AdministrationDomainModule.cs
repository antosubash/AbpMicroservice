using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AdministrationDomainSharedModule)
)]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
    public class AdministrationDomainModule : AbpModule
{

}
