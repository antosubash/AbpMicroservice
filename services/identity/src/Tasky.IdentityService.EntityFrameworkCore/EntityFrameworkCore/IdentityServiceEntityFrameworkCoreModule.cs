using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Tasky.IdentityService.EntityFrameworkCore;

[DependsOn(
        typeof(IdentityServiceDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule)
)]
public class IdentityServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
            context.Services.AddAbpDbContext<IdentityServiceDbContext>(options =>
            {
                options.ReplaceDbContext<IIdentityDbContext>();
                options.ReplaceDbContext<IIdentityServerDbContext>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });
    }
}
