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
                options.Databases.Configure("SaasService", database =>
                {
                    database.MappedConnections.Add("AbpTenantManagement");
                    database.IsUsedByTenants = false;
                });

                options.Databases.Configure("AdministrationService", database =>
                {
                    database.MappedConnections.Add("AbpAuditLogging");
                    database.MappedConnections.Add("AbpPermissionManagement");
                    database.MappedConnections.Add("AbpSettingManagement");
                    database.MappedConnections.Add("AbpFeatureManagement");
                });
                
                options.Databases.Configure("IdentityService", database =>
                {
                    database.MappedConnections.Add("AbpIdentity");
                    database.MappedConnections.Add("AbpIdentityServer");
                });
                
                options.Databases.Configure("ProjectService", database =>
                {
                    database.MappedConnections.Add("ProjectService");
                });
        });
    }
}