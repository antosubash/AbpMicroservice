using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Tasky.Administration.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.PermissionManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AbpValidationModule)
)]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
    public class AdministrationDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdministrationDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AdministrationResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/Administration");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Administration", typeof(AdministrationResource));
        });
    }
}
