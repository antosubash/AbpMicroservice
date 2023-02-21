using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.Projects.EntityFrameworkCore;

[DependsOn(
    typeof(ProjectsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ProjectsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ProjectsDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
        });
    }
}