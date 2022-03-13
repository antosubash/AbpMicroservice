using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tasky.Administration.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using Tasky.Microservice.Shared;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Data;
using Tasky.IdentityService;
using Tasky.SaaS;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationApplicationModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationHttpApiModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(SaaSApplicationContractsModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(TaskyMicroserviceModule)
    )]
public class AdministrationHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<AdministrationDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Tasky.Administration.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<AdministrationDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Tasky.Administration.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<AdministrationApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Tasky.Administration.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<AdministrationApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Tasky.Administration.Application", Path.DirectorySeparatorChar)));
            });
        }

        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                {"AdministrationService", "AdministrationService API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Administration API", Version = "v1"});
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });


        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "AdministrationService";
            });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Administration:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Administration");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Administration-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseMultiTenancy();

        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("Administration");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
