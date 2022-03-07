namespace Tasky.Microservice.Shared;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class TaskyMicroserviceModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure("Administration", database =>
            {
                database.MappedConnections.Add("AbpAuditLogging");
                database.MappedConnections.Add("AbpPermissionManagement");
                database.MappedConnections.Add("AbpSettingManagement");
                database.MappedConnections.Add("AbpFeatureManagement");
            });
        });
    }
}