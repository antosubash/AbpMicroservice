using Tasky.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tasky.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TaskyEntityFrameworkCoreModule),
    typeof(TaskyApplicationContractsModule)
    )]
public class TaskyDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
