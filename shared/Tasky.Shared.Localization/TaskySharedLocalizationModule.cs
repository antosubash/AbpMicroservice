using Tasky.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tasky
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class TaskySharedLocalizationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TaskySharedLocalizationModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<TaskyResource>("en")
                    .AddBaseTypes(
                        typeof(AbpValidationResource)
                    ).AddVirtualJson("/Localization/Tasky");

                options.DefaultResourceType = typeof(TaskyResource);
            });
        }
    }
}