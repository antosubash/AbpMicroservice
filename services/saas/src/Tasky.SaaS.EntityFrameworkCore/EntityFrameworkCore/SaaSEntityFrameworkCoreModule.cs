using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tasky.SaaS.EntityFrameworkCore;

[DependsOn(
    typeof(SaaSDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class SaaSEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SaaSDbContext>(options =>
        {
            options.ReplaceDbContext<ITenantManagementDbContext>();
            
            /* includeAllEntities: true allows to use IRepository<TEntity, TKey> also for non aggregate root entities */
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
