using Tasky.Administration;
using Tasky.Administration.EntityFrameworkCore;
using Tasky.IdentityService;
using Tasky.IdentityService.EntityFrameworkCore;
using Tasky.Microservice.Shared;
using Tasky.SaaS;
using Tasky.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Tasky.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(SaaSApplicationContractsModule),
    typeof(TaskyMicroserviceModule)
)]
public class TaskyDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}