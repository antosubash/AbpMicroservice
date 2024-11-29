using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Tasky.WebApp;

[DependsOn(typeof(WebAppDomainModule))]
[DependsOn(typeof(AbpAccountApplicationModule))]
[DependsOn(typeof(WebAppApplicationContractsModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpTenantManagementApplicationModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
public class WebAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WebAppApplicationModule>();
        });
    }
}
