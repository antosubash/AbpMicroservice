$name = $args[0]

dotnet new web -n "$name.IdentityServer" -o "apps\$name.IdentityServer"
dotnet new web -n "$name.Gateway" -o "gateway\$name.Gateway"
dotnet new classlib -n "$name.Shared.Hosting" -o "shared\$name.Shared.Hosting"
dotnet new console -n "$name.DbMigrator" -o "shared\$name.DbMigrator"
abp new "$name.AdministrationService" -t module --no-ui -o services\administration
abp new "$name.IdentityService" -t module --no-ui -o services\identity
abp new "$name.SaaSService" -t module --no-ui -o services\saas
dotnet new sln -n "$name"
dotnet sln ".\$name.sln" add (ls -r **/*.csproj)
abp new "$name" -t app -u angular -dbms PostgreSQL -m none --separate-identity-server --database-provider ef -csf -o temp
Move-Item -Path ".\temp\$name\angular\" -Destination .\apps\angular
Move-Item -Path ".\temp\$name\aspnet-core\src\$name.DbMigrator" -Destination .\shared\ -Force
Move-Item -Path ".\temp\$name\aspnet-core\src\$name.IdentityServer" -Destination .\apps\ -Force
Remove-Item -Recurse -Force .\temp\ 
dotnet sln ".\$name.sln" remove (ls -r **/*.Installer.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.Host.Shared.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.MongoDB.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.MongoDB.Tests.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.AdministrationService.IdentityServer.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.IdentityService.IdentityServer.csproj)
dotnet sln ".\$name.sln" remove (ls -r **/*.SaaSService.IdentityServer.csproj)
Remove-Item -Recurse -Force (ls -r **/*.SaaSService.IdentityServer)
Remove-Item -Recurse -Force (ls -r **/*.IdentityService.IdentityServer)
Remove-Item -Recurse -Force (ls -r **/*.AdministrationService.IdentityServer)
Remove-Item -Recurse -Force (ls -r **/*.MongoDB.Tests)
Remove-Item -Recurse -Force (ls -r **/*.MongoDB)
Remove-Item -Recurse -Force (ls -r **/*.Host.Shared)
Remove-Item -Recurse -Force (ls -r **/*.Installer)
abp add-module Volo.AuditLogging -s "services\administration\$name.AdministrationService.sln" --skip-db-migrations
abp add-module Volo.FeatureManagement -s "services\administration\$name.AdministrationService.sln" --skip-db-migrations
abp add-module Volo.PermissionManagement -s "services\administration\$name.AdministrationService.sln" --skip-db-migrations
abp add-module Volo.SettingManagement -s "services\administration\$name.AdministrationService.sln" --skip-db-migrations

abp add-module Volo.Identity -s "services\identity\$name.IdentityService.sln" --skip-db-migrations
abp add-module Volo.IdentityServer -s "services\identity\$name.IdentityService.sln" --skip-db-migrations

abp add-module Volo.TenantManagement -s "services\saas\$name.SaaSService.sln" --skip-db-migrations
