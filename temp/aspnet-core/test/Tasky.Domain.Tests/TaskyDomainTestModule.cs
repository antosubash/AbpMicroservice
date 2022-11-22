using Tasky.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky;

[DependsOn(
    typeof(TaskyEntityFrameworkCoreTestModule)
    )]
public class TaskyDomainTestModule : AbpModule
{

}
