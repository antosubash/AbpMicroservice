# IdentityService service

The module template will need little bit clean up. lets delete few folders we will not use.

- database
- Tasky.IdentityService.Host.Shared
- Tasky.IdentityService.IdentityServer
- Tasky.IdentityService.Installer
- Tasky.IdentityService.MongoDB
- Tasky.IdentityService.MongoDB.Tests

We will delete these folders and also remove them from the solution folder.

## Add the shared project as a reference to the host

```xml
<ProjectReference Include="..\..\..\..\shared\Tasky.Shared.Hosting\Tasky.Shared.Hosting.csproj" />
```

## Update the connection string

`User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=IdentityService;Pooling=false;`

## Update the `IdentityServiceHttpApiHostModule`

Update the depends on.

```cs
[DependsOn(
    typeof(TaskyHostingModule),
    typeof(IdentityServiceApplicationModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceHttpApiModule),
)]
```

Remove the things which are configured in the shared project.

## Create the `DbContextFactory` in the EntityFrameworkCore project

```cs
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.IdentityService.EntityFrameworkCore;

public class IdentityServiceDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceDbContext>
{
    public IdentityServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<IdentityServiceDbContext>()
            .UseNpgsql(GetConnectionStringFromConfiguration());

        return new IdentityServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(IdentityServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory())?.Parent!.FullName!,
                    $"host{Path.DirectorySeparatorChar}Tasky.IdentityService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}
```

Once this is created delete `EntityFrameworkCore` folder can be created.

## Migration

To create migrations

`dotnet ef migrations add Init`

To update database

`dotnet ef database update`
