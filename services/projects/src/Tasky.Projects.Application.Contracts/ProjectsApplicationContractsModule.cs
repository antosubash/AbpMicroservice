using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tasky.Projects;

[DependsOn(
    typeof(ProjectsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ProjectsApplicationContractsModule : AbpModule
{

}
