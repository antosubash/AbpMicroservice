# Setup the shared project

## Nuget packages

This is a shared project which will be shared by all the services.

Lets install all the shared nuget packages.

```xml
<PackageReference Include="Volo.Abp.Autofac" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.EventBus.RabbitMQ" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.Localization" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.MultiTenancy" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy" Version="5.1.4"/>
<PackageReference Include="Serilog.AspNetCore" Version="4.1.0"/>
<PackageReference Include="Serilog.Sinks.Async" Version="1.5.0"/>
<PackageReference Include="IdentityModel" Version="5.1.0"/>
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.0"/>
<PackageReference Include="Microsoft.AspNetCore.DataProtection.StackExchangeRedis" Version="6.0.0"/>
<PackageReference Include="Volo.Abp.Caching.StackExchangeRedis" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.Http.Client.IdentityModel.Web" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.Identity.HttpApi.Client" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.AspNetCore.Serilog" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.Swashbuckle" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.EntityFrameworkCore" Version="5.1.4"/>
<PackageReference Include="Volo.Abp.EntityFrameworkCore.PostgreSql" Version="5.1.4"/>
```

## Shared module

Lets create a shared `TaskyHostingModule`.

```cs
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;

namespace Tasky.Shared.Hosting;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpDataModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
)]
public class TaskyHostingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure("SaaSService", database =>
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
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }
}
```
