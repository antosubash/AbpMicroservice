using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.SaaS;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SaaSDomainSharedModule)
)]
public class SaaSDomainModule : AbpModule
{

}
