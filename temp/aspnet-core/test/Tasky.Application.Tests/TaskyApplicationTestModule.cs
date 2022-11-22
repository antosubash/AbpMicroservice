using Volo.Abp.Modularity;

namespace Tasky;

[DependsOn(
    typeof(TaskyApplicationModule),
    typeof(TaskyDomainTestModule)
    )]
public class TaskyApplicationTestModule : AbpModule
{

}
