using Volo.Abp.Modularity;

namespace Tasky.Projects;

[DependsOn(typeof(ProjectsApplicationModule))]
[DependsOn(typeof(ProjectsDomainTestModule))]
public class ProjectsApplicationTestModule : AbpModule
{

}
