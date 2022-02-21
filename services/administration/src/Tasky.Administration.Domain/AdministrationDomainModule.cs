using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AdministrationDomainSharedModule)
)]
public class AdministrationDomainModule : AbpModule
{

}
