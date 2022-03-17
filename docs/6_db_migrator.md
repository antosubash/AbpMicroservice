# DB Migrations

## Add the references

For migrations we need to first projects as a reference in the db migrator

```xml
    <ItemGroup>
        <ProjectReference Include="..\..\services\AdministrationService\src\Tasky.AdministrationService.Application.Contracts\Tasky.AdministrationService.Application.Contracts.csproj"/>
        <ProjectReference Include="..\..\services\AdministrationService\src\Tasky.AdministrationService.EntityFrameworkCore\Tasky.AdministrationService.EntityFrameworkCore.csproj"/>
        <ProjectReference Include="..\..\services\identity\src\Tasky.IdentityService.Application.Contracts\Tasky.IdentityService.Application.Contracts.csproj"/>
        <ProjectReference Include="..\..\services\identity\src\Tasky.IdentityService.EntityFrameworkCore\Tasky.IdentityService.EntityFrameworkCore.csproj"/>
        <ProjectReference Include="..\..\services\SaaSService\src\Tasky.SaaSService.Application.Contracts\Tasky.SaaSService.Application.Contracts.csproj"/>
        <ProjectReference Include="..\..\services\SaaSService\src\Tasky.SaaSService.EntityFrameworkCore\Tasky.SaaSService.EntityFrameworkCore.csproj"/>
        <ProjectReference Include="..\Tasky.Microservice.Shared\Tasky.Microservice.Shared.csproj"/>
    </ItemGroup>
```

We are adding `EntityFrameworkCore` and `Contracts` projects to the DbMigrations project.

## Update the packages

```xml
    <ItemGroup>
        <PackageReference Include="Serilog.Extensions.Logging" Version="3.0.1"/>
        <PackageReference Include="Serilog.Sinks.Async" Version="1.5.0"/>
        <PackageReference Include="Serilog.Sinks.File" Version="5.0.0"/>
        <PackageReference Include="Serilog.Sinks.Console" Version="4.0.0"/>
        <PackageReference Include="Microsoft.Extensions.Hosting" Version="6.0.0"/>
    </ItemGroup>
```

## Create `DbMigrationService`

```cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tasky.AdministrationService.EntityFrameworkCore;
using Tasky.IdentityService.EntityFrameworkCore;
using Tasky.SaaSService.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace Tasky.DbMigrator;

public class TaskyDbMigrationService : ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IDataSeeder _dataSeeder;
    private readonly ILogger<TaskyDbMigrationService> _logger;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public TaskyDbMigrationService(
        ILogger<TaskyDbMigrationService> logger,
        ITenantRepository tenantRepository,
        IDataSeeder dataSeeder,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _logger = logger;
        _tenantRepository = tenantRepository;
        _dataSeeder = dataSeeder;
        _currentTenant = currentTenant;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await MigrateHostAsync(cancellationToken);
        await MigrateTenantsAsync(cancellationToken);
        _logger.LogInformation("Migration completed!");
    }

    private async Task MigrateHostAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrating Host side...");
        await MigrateAllDatabasesAsync(null, cancellationToken);
        await SeedDataAsync();
    }

    private async Task MigrateTenantsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrating tenants...");

        var tenants =
            await _tenantRepository.GetListAsync(includeDetails: true, cancellationToken: cancellationToken);
        var migratedDatabaseSchemas = new HashSet<string>();
        foreach (var tenant in tenants)
        {
            using (_currentTenant.Change(tenant.Id))
            {
                // Database schema migration
                var connectionString = tenant.FindDefaultConnectionString();
                if (!connectionString.IsNullOrWhiteSpace() && //tenant has a separate database
                    !migratedDatabaseSchemas.Contains(connectionString)) //the database was not migrated yet
                {
                    _logger.LogInformation($"Migrating tenant database: {tenant.Name} ({tenant.Id})");
                    await MigrateAllDatabasesAsync(tenant.Id, cancellationToken);
                    migratedDatabaseSchemas.AddIfNotContains(connectionString);
                }

                //Seed data
                _logger.LogInformation($"Seeding tenant data: {tenant.Name} ({tenant.Id})");
                await SeedDataAsync();
            }
        }
    }

    private async Task MigrateAllDatabasesAsync(
        Guid? tenantId,
        CancellationToken cancellationToken)
    {
        using (var uow = _unitOfWorkManager.Begin(true))
        {
            if (tenantId == null)
            {
                /* SaaSService schema should only be available in the host side */
                await MigrateDatabaseAsync<SaaSServiceDbContext>(cancellationToken);
            }

            await MigrateDatabaseAsync<AdministrationServiceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<IdentityServiceDbContext>(cancellationToken);

            await uow.CompleteAsync(cancellationToken);
        }

        _logger.LogInformation(
            $"All databases have been successfully migrated ({(tenantId.HasValue ? $"tenantId: {tenantId}" : "HOST")}).");
    }

    private async Task MigrateDatabaseAsync<TDbContext>(
        CancellationToken cancellationToken)
        where TDbContext : DbContext, IEfCoreDbContext
    {
        _logger.LogInformation($"Migrating {typeof(TDbContext).Name.RemovePostFix("DbContext")} database...");

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext
            .Database
            .MigrateAsync(cancellationToken);
    }

    private async Task SeedDataAsync()
    {
        await _dataSeeder.SeedAsync(
            new DataSeedContext(_currentTenant.Id)
                .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, "admin@abp.io")
                .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, "1q2w3E*")
        );
    }
}
```

This service will migrate all the databases of the base services and also seed the data for the data seeder.

## Update appsettings.json

```xml
{
  "ConnectionStrings": {
    "SaaSService": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=SaaSServiceService;Pooling=false;",
    "IdentityService": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=IdentityService;Pooling=false;",
    "AdministrationService": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=AdministrationService;Pooling=false;"
  },
  "ApiScope": [
    "AuthServer",
    "SaaSServiceService",
    "IdentityService",
    "AdministrationService"
  ],
  "ApiResource": [
    "AuthServer",
    "SaaSServiceService",
    "IdentityService",
    "AdministrationService"
  ],
  "Clients": [
    {
      "ClientId": "Tasky_Web",
      "ClientSecret": "1q2w3e*",
      "RootUrls": [
        "https://localhost:7004"
      ],
      "Scopes": [
        "SaaSServiceService",
        "IdentityService",
        "AdministrationService"
      ],
      "GrantTypes": [
        "hybrid"
      ],
      "RedirectUris": [
        "https://localhost:7004/signin-oidc"
      ],
      "PostLogoutRedirectUris": [
        "https://localhost:7004/signout-callback-oidc"
      ],
      "AllowedCorsOrigins": [
        "https://localhost:7004"
      ]
    },
    {
      "ClientId": "Tasky_App",
      "ClientSecret": "1q2w3e*",
      "RootUrls": [
        "http://localhost:4200"
      ],
      "Scopes": [
        "AuthServer",
        "SaaSServiceService",
        "IdentityService",
        "AdministrationService"
      ],
      "GrantTypes": [
        "authorization_code"
      ],
      "RedirectUris": [
        "https://localhost:4200"
      ],
      "PostLogoutRedirectUris": [
        "http://localhost:4200"
      ],
      "AllowedCorsOrigins": [
        "http://localhost:4200"
      ]
    },
    {
      "ClientId": "AdministrationService_Swagger",
      "ClientSecret": "1q2w3e*",
      "RootUrls": [
        "https://localhost:7001"
      ],
      "Scopes": [
        "SaaSServiceService",
        "IdentityService",
        "AdministrationService"
      ],
      "GrantTypes": [
        "authorization_code"
      ],
      "RedirectUris": [
        "https://localhost:7001/swagger/oauth2-redirect.html"
      ],
      "PostLogoutRedirectUris": [
        "https://localhost:7001/signout-callback-oidc"
      ],
      "AllowedCorsOrigins": [
        "https://localhost:7001"
      ]
    },
    {
      "ClientId": "IdentityService_Swagger",
      "ClientSecret": "1q2w3e*",
      "RootUrls": [
        "https://localhost:7002"
      ],
      "Scopes": [
        "SaaSServiceService",
        "IdentityService",
        "AdministrationService"
      ],
      "GrantTypes": [
        "authorization_code"
      ],
      "RedirectUris": [
        "https://localhost:7002/swagger/oauth2-redirect.html"
      ],
      "PostLogoutRedirectUris": [
        "https://localhost:7002"
      ],
      "AllowedCorsOrigins": [
        "https://localhost:7002"
      ]
    },
    {
      "ClientId": "SaaSService_Swagger",
      "ClientSecret": "1q2w3e*",
      "RootUrls": [
        "https://localhost:7003"
      ],
      "Scopes": [
        "SaaSServiceService",
        "IdentityService",
        "AdministrationService"
      ],
      "GrantTypes": [
        "authorization_code"
      ],
      "RedirectUris": [
        "https://localhost:7003/swagger/oauth2-redirect.html"
      ],
      "PostLogoutRedirectUris": [
        "https://localhost:7003"
      ],
      "AllowedCorsOrigins": [
        "https://localhost:7003"
      ]
    }
  ]
}
```

## Create `IdentityServerDataSeedContributor` for identity server

```cs
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tasky.DbMigrator;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityServerDataSeeder _identityServerDataSeeder;

    public IdentityServerDataSeedContributor(IdentityServerDataSeeder identityServerDataSeeder)
    {
        _identityServerDataSeeder = identityServerDataSeeder;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        await _identityServerDataSeeder.SeedAsync();
    }
}
```

## Create `IdentityServerDataSeeder` for reading json and create resource

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace Tasky.DbMigrator;

public class IdentityServerDataSeeder : ITransientDependency
{
    private readonly IApiResourceRepository _apiResourceRepository;
    private readonly IApiScopeRepository _apiScopeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IConfiguration _configuration;
    private readonly ICurrentTenant _currentTenant;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
    private readonly IPermissionDataSeeder _permissionDataSeeder;

    public IdentityServerDataSeeder(
        IClientRepository clientRepository,
        IApiResourceRepository apiResourceRepository,
        IApiScopeRepository apiScopeRepository,
        IIdentityResourceDataSeeder identityResourceDataSeeder,
        IGuidGenerator guidGenerator,
        IPermissionDataSeeder permissionDataSeeder,
        IConfiguration configuration,
        ICurrentTenant currentTenant)
    {
        _clientRepository = clientRepository;
        _apiResourceRepository = apiResourceRepository;
        _apiScopeRepository = apiScopeRepository;
        _identityResourceDataSeeder = identityResourceDataSeeder;
        _guidGenerator = guidGenerator;
        _permissionDataSeeder = permissionDataSeeder;
        _configuration = configuration;
        _currentTenant = currentTenant;
    }

    [UnitOfWork]
    public async virtual Task SeedAsync()
    {
        using (_currentTenant.Change(null))
        {
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
            await CreateApiScopesAsync();
            await CreateClientsAsync();
        }
    }

    private async Task CreateClientsAsync()
    {
        var clients = _configuration.GetSection("Clients").Get<List<ServiceClient>>();
        var commonScopes = new[] {
            "email",
            "openid",
            "profile",
            "role",
            "phone",
            "address"
        };

        foreach (var client in clients)
        {
            await CreateClientAsync(
                client.ClientId,
                commonScopes.Union(client.Scopes),
                client.GrantTypes,
                client.ClientSecret.Sha256(),
                requireClientSecret: false,
                redirectUris: client.RedirectUris,
                postLogoutRedirectUris: client.PostLogoutRedirectUris,
                corsOrigins: client.AllowedCorsOrigins
            );
        }
    }


    private async Task CreateApiResourcesAsync()
    {
        var commonApiUserClaims = new[] {
            "email",
            "email_verified",
            "name",
            "phone_number",
            "phone_number_verified",
            "role"
        };

        var apiResources = _configuration.GetSection("ApiResource").Get<string[]>();

        foreach (var item in apiResources)
        {
            await CreateApiResourceAsync(item, commonApiUserClaims);
        }
    }

    private async Task CreateApiScopesAsync()
    {
        var apiScopes = _configuration.GetSection("ApiScope").Get<string[]>();
        foreach (var item in apiScopes)
        {
            await CreateApiScopeAsync(item);
        }
    }

    private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
    {
        var apiResource = await _apiResourceRepository.FindByNameAsync(name);
        if (apiResource == null)
        {
            apiResource = await _apiResourceRepository.InsertAsync(
                new ApiResource(
                    _guidGenerator.Create(),
                    name,
                    name + " API"
                ),
                true
            );
        }

        foreach (var claim in claims)
        {
            if (apiResource.FindClaim(claim) == null)
            {
                apiResource.AddUserClaim(claim);
            }
        }

        return await _apiResourceRepository.UpdateAsync(apiResource);
    }

    private async Task<ApiScope> CreateApiScopeAsync(string name)
    {
        var apiScope = await _apiScopeRepository.FindByNameAsync(name);
        if (apiScope == null)
        {
            apiScope = await _apiScopeRepository.InsertAsync(
                new ApiScope(
                    _guidGenerator.Create(),
                    name,
                    name + " API"
                ),
                true
            );
        }

        return apiScope;
    }

    private async Task<Client> CreateClientAsync(
        string name,
        IEnumerable<string> scopes,
        IEnumerable<string> grantTypes,
        string secret = null,
        IEnumerable<string> redirectUris = null,
        IEnumerable<string> postLogoutRedirectUris = null,
        string frontChannelLogoutUri = null,
        bool requireClientSecret = true,
        bool requirePkce = false,
        IEnumerable<string> permissions = null,
        IEnumerable<string> corsOrigins = null)
    {
        var client = await _clientRepository.FindByClientIdAsync(name);
        if (client == null)
        {
            client = await _clientRepository.InsertAsync(
                new Client(
                    _guidGenerator.Create(),
                    name
                ) {
                    ClientName = name,
                    ProtocolType = "oidc",
                    Description = name,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AbsoluteRefreshTokenLifetime = 31536000, //365 days
                    AccessTokenLifetime = 31536000, //365 days
                    AuthorizationCodeLifetime = 300,
                    IdentityTokenLifetime = 300,
                    RequireConsent = false,
                    FrontChannelLogoutUri = frontChannelLogoutUri,
                    RequireClientSecret = requireClientSecret,
                    RequirePkce = requirePkce
                },
                true
            );
        }

        foreach (var scope in scopes)
        {
            if (client.FindScope(scope) == null)
            {
                client.AddScope(scope);
            }
        }

        foreach (var grantType in grantTypes)
        {
            if (client.FindGrantType(grantType) == null)
            {
                client.AddGrantType(grantType);
            }
        }

        if (!secret.IsNullOrEmpty())
        {
            if (client.FindSecret(secret) == null)
            {
                client.AddSecret(secret);
            }
        }

        foreach (var redirectUrl in redirectUris)
        {
            if (client.FindRedirectUri(redirectUrl) == null)
            {
                client.AddRedirectUri(redirectUrl);
            }
        }

        foreach (var postLogoutRedirectUri in postLogoutRedirectUris)
        {
            if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
            {
                client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
            }
        }

        if (permissions != null)
        {
            await _permissionDataSeeder.SeedAsync(
                ClientPermissionValueProvider.ProviderName,
                name,
                permissions
            );
        }

        if (corsOrigins != null)
        {
            foreach (var origin in corsOrigins)
            {
                if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                {
                    client.AddCorsOrigin(origin);
                }
            }
        }

        return await _clientRepository.UpdateAsync(client);
    }
}
```
