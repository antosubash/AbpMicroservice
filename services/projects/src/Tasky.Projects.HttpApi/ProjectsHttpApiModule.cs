using Localization.Resources.AbpUi;
using Tasky.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tasky.Projects;

[DependsOn(
    typeof(ProjectsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ProjectsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ProjectsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ProjectsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
