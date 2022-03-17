# Identity server

## Add project reference

```xml
    <ItemGroup>
        <ProjectReference Include="..\..\services\administration\src\Tasky.AdministrationService.EntityFrameworkCore\Tasky.AdministrationService.EntityFrameworkCore.csproj" />
        <ProjectReference Include="..\..\services\identity\src\Tasky.IdentityService.EntityFrameworkCore\Tasky.IdentityService.EntityFrameworkCore.csproj" />
        <ProjectReference Include="..\..\services\saas\src\Tasky.SaaSService.EntityFrameworkCore\Tasky.SaaSService.EntityFrameworkCore.csproj" />
        <ProjectReference Include="..\..\shared\Tasky.Shared.Hosting\Tasky.Shared.Hosting.csproj" />
    </ItemGroup>
```

## Update appsetting.json

```xml
  "ConnectionStrings": {
    "SaaS": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=SaasService;Pooling=false;",
    "IdentityService": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=IdentityService;Pooling=false;",
    "Administration": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=AdministrationService;Pooling=false;"
  },
```

## Update the depends on

```xml
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(TaskyHostingModule)
```

## Angular

lets update the environment for the angular app.

```ts
import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Tasky',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7000',
    redirectUri: baseUrl,
    clientId: 'Tasky_App',
    responseType: 'code',
    scope: 'offline_access IdentityService AdministrationService SaaSService role email openid profile',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'Tasky',
    },
    Tasky: {
      url: 'https://localhost:44350',
      rootNamespace: 'Tasky',
    },
  },
} as Environment;
```
