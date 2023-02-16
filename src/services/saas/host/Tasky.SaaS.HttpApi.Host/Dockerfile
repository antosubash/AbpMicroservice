#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/host/Tasky.SaaS.HttpApi.Host/Tasky.SaaS.HttpApi.Host.csproj", "services/saas/host/Tasky.SaaS.HttpApi.Host/"]
COPY ["services/saas/src/Tasky.SaaS.EntityFrameworkCore/Tasky.SaaS.EntityFrameworkCore.csproj", "services/saas/src/Tasky.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/Tasky.SaaS.Domain/Tasky.SaaS.Domain.csproj", "services/saas/src/Tasky.SaaS.Domain/"]
COPY ["services/saas/src/Tasky.SaaS.Domain.Shared/Tasky.SaaS.Domain.Shared.csproj", "services/saas/src/Tasky.SaaS.Domain.Shared/"]
COPY ["services/saas/src/Tasky.SaaS.Application/Tasky.SaaS.Application.csproj", "services/saas/src/Tasky.SaaS.Application/"]
COPY ["services/saas/src/Tasky.SaaS.Application.Contracts/Tasky.SaaS.Application.Contracts.csproj", "services/saas/src/Tasky.SaaS.Application.Contracts/"]
COPY ["shared/Tasky.Hosting.Shared/Tasky.Hosting.Shared.csproj", "shared/Tasky.Hosting.Shared/"]
COPY ["services/administration/src/Tasky.Administration.EntityFrameworkCore/Tasky.Administration.EntityFrameworkCore.csproj", "services/administration/src/Tasky.Administration.EntityFrameworkCore/"]
COPY ["services/administration/src/Tasky.Administration.Domain/Tasky.Administration.Domain.csproj", "services/administration/src/Tasky.Administration.Domain/"]
COPY ["services/administration/src/Tasky.Administration.Domain.Shared/Tasky.Administration.Domain.Shared.csproj", "services/administration/src/Tasky.Administration.Domain.Shared/"]
COPY ["services/saas/src/Tasky.SaaS.HttpApi/Tasky.SaaS.HttpApi.csproj", "services/saas/src/Tasky.SaaS.HttpApi/"]
RUN dotnet restore "services/saas/host/Tasky.SaaS.HttpApi.Host/Tasky.SaaS.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/host/Tasky.SaaS.HttpApi.Host"
RUN dotnet build "Tasky.SaaS.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.SaaS.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.SaaS.HttpApi.Host.dll"]