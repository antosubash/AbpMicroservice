using Tasky.Administration.EntityFrameworkCore;
using Tasky.Hosting.Shared;
using Volo.Abp.Modularity;

namespace Tasky.Microservice.Shared;

[DependsOn(
    typeof(TaskyHostingModule),
    typeof(AdministrationEntityFrameworkCoreModule)
)]
public class TaskyMicroserviceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}