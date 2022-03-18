# Intro

This is document to setup a abp microservice application from the beginning.

## Easy way

Copy the following command into a powershell file and run it.

```bash
dotnet new web -n Tasky.IdentityServer -o apps\Tasky.IdentityServer
dotnet new web -n Tasky.Gateway -o gateway\Tasky.Gateway
dotnet new classlib -n Tasky.Shared.Hosting -o shared\Tasky.Shared.Hosting
dotnet new console -n Tasky.DbMigrator -o shared\Tasky.DbMigrator
abp new Tasky.AdministrationService -t module --no-ui -o services\administration
abp new Tasky.IdentityService -t module --no-ui -o services\identity
abp new Tasky.SaaSService -t module --no-ui -o services\saas
dotnet new sln -n Tasky
dotnet sln .\Tasky.sln add (ls -r **/*.csproj)
abp new Tasky -t app -u angular -dbms PostgreSQL -m none --separate-identity-server --database-provider ef -csf -o temp
Move-Item -Path .\temp\Tasky\angular\ -Destination .\apps\angular
Move-Item -Path .\temp\Tasky\aspnet-core\src\Tasky.DbMigrator -Destination .\shared\ -Force
Move-Item -Path .\temp\Tasky\aspnet-core\src\Tasky.IdentityServer -Destination .\apps\ -Force
Remove-Item -Recurse -Force .\temp\
```

## Hard way

We can also create things manually

```bash
mkdir services, services\administration, services\identity, services\saas, gateway, apps, shared, temp 
```

`services` folder is to store all our services

`gateway` folder is to store our gateway

`shared` folder is to store the shared code for all the services

`apps` folder is for `angular` and `IdentityServer` applications

`temp` folder is to store the temp tired application

## 2. Creating the temp project in the temp folders

We are creating this temp project so that we can use some of the projects available in the app.

```bash
abp new Tasky -t app -u angular -dbms PostgreSQL -m none --separate-identity-server --database-provider ef -csf
```

## 3. Move the temp projects

Lets move the required applications.

Move `angular` and `Tasky.IdentityServer` to the `apps` folder.
Move `Tasky.DbMigrator` to the shared project.

## 4. Create solution file

```bash
dotnet new sln -n Tasky
```

This will generate an empty solution file.

## 5. Create gateway project

We will use `Yarp` as reverse proxy. we will place this projects in `gateway` folder

```bash
dotnet new web -n Tasky.Gateway
```

## 6. Create Shared project

we will create this in the `shared` folder.

```bash
dotnet new classlib -n Tasky.Shared.Hosting
```

## 7. Create services

we will create these in the `services` folder

### For creating Administration service

lets create a folder for `administration` service

```bash
mkdir administration
```

```bash
abp new Tasky.AdministrationService -t module --no-ui -o services\administration
```

### For creating Identity service

lets create a folder for `identity` service

```bash
mkdir identity
```

```bash
abp new Tasky.IdentityService -t module --no-ui -o services\identity
```

### For creating SaaS service

lets create a folder for `saas`

```bash
mkdir saas
```

```bash
abp new Tasky.SaaSService -t module --no-ui -o services\saas
```

## 8. Adding projects to Tasky solution

Add all the projects we created into the solution.

## 9. Clean up

delete the temp folder.
