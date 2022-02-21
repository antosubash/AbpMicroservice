using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.FeatureManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
    [DependsOn(typeof(AbpSettingManagementApplicationModule))]
    [DependsOn(typeof(AbpFeatureManagementApplicationModule))]
    public class AdministrationApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdministrationApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdministrationApplicationModule>(validate: true);
        });
    }
}
