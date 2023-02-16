#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/host/Tasky.IdentityService.HttpApi.Host/Tasky.IdentityService.HttpApi.Host.csproj", "services/identity/host/Tasky.IdentityService.HttpApi.Host/"]
COPY ["services/identity/src/Tasky.IdentityService.HttpApi/Tasky.IdentityService.HttpApi.csproj", "services/identity/src/Tasky.IdentityService.HttpApi/"]
COPY ["services/identity/src/Tasky.IdentityService.Application.Contracts/Tasky.IdentityService.Application.Contracts.csproj", "services/identity/src/Tasky.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/Tasky.IdentityService.Domain.Shared/Tasky.IdentityService.Domain.Shared.csproj", "services/identity/src/Tasky.IdentityService.Domain.Shared/"]
COPY ["services/saas/src/Tasky.SaaS.EntityFrameworkCore/Tasky.SaaS.EntityFrameworkCore.csproj", "services/saas/src/Tasky.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/Tasky.SaaS.Domain/Tasky.SaaS.Domain.csproj", "services/saas/src/Tasky.SaaS.Domain/"]
COPY ["services/saas/src/Tasky.SaaS.Domain.Shared/Tasky.SaaS.Domain.Shared.csproj", "services/saas/src/Tasky.SaaS.Domain.Shared/"]
COPY ["services/identity/src/Tasky.IdentityService.Application/Tasky.IdentityService.Application.csproj", "services/identity/src/Tasky.IdentityService.Application/"]
COPY ["services/identity/src/Tasky.IdentityService.Domain/Tasky.IdentityService.Domain.csproj", "services/identity/src/Tasky.IdentityService.Domain/"]
COPY ["services/identity/src/Tasky.IdentityService.EntityFrameworkCore/Tasky.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/Tasky.IdentityService.EntityFrameworkCore/"]
COPY ["shared/Tasky.Hosting.Shared/Tasky.Hosting.Shared.csproj", "shared/Tasky.Hosting.Shared/"]
COPY ["services/administration/src/Tasky.Administration.EntityFrameworkCore/Tasky.Administration.EntityFrameworkCore.csproj", "services/administration/src/Tasky.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/Tasky.Administration.Domain/Tasky.Administration.Domain.csproj", "services/administration/src/Tasky.Administration.Domain/"]
COPY ["services/administration/src/Tasky.Administration.Domain.Shared/Tasky.Administration.Domain.Shared.csproj", "services/administration/src/Tasky.Administration.Domain.Shared/"]
RUN dotnet restore "services/identity/host/Tasky.IdentityService.HttpApi.Host/Tasky.IdentityService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/identity/host/Tasky.IdentityService.HttpApi.Host"
RUN dotnet build "Tasky.IdentityService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.IdentityService.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.IdentityService.HttpApi.Host.dll"]