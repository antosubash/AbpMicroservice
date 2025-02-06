# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/projects/host/Tasky.Projects.HttpApi.Host/Tasky.Projects.HttpApi.Host.csproj", "services/projects/host/Tasky.Projects.HttpApi.Host/"]
COPY ["shared/Tasky.Microservice.Shared/Tasky.Microservice.Shared.csproj", "shared/Tasky.Microservice.Shared/"]
COPY ["services/administration/src/Tasky.Administration.EntityFrameworkCore/Tasky.Administration.EntityFrameworkCore.csproj", "services/administration/src/Tasky.Administration.EntityFrameworkCore/"]
COPY ["shared/Tasky.Shared/Tasky.Shared.csproj", "shared/Tasky.Shared/"]
COPY ["services/administration/src/Tasky.Administration.Domain/Tasky.Administration.Domain.csproj", "services/administration/src/Tasky.Administration.Domain/"]
COPY ["services/administration/src/Tasky.Administration.Domain.Shared/Tasky.Administration.Domain.Shared.csproj", "services/administration/src/Tasky.Administration.Domain.Shared/"]
COPY ["shared/Tasky.Hosting.Shared/Tasky.Hosting.Shared.csproj", "shared/Tasky.Hosting.Shared/"]
COPY ["shared/Tasky.ServiceDefaults/Tasky.ServiceDefaults.csproj", "shared/Tasky.ServiceDefaults/"]
COPY ["services/projects/src/Tasky.Projects.HttpApi/Tasky.Projects.HttpApi.csproj", "services/projects/src/Tasky.Projects.HttpApi/"]
COPY ["services/projects/src/Tasky.Projects.Application.Contracts/Tasky.Projects.Application.Contracts.csproj", "services/projects/src/Tasky.Projects.Application.Contracts/"]
COPY ["services/projects/src/Tasky.Projects.Domain.Shared/Tasky.Projects.Domain.Shared.csproj", "services/projects/src/Tasky.Projects.Domain.Shared/"]
COPY ["services/projects/src/Tasky.Projects.Application/Tasky.Projects.Application.csproj", "services/projects/src/Tasky.Projects.Application/"]
COPY ["services/projects/src/Tasky.Projects.Domain/Tasky.Projects.Domain.csproj", "services/projects/src/Tasky.Projects.Domain/"]
COPY ["services/projects/src/Tasky.Projects.EntityFrameworkCore/Tasky.Projects.EntityFrameworkCore.csproj", "services/projects/src/Tasky.Projects.EntityFrameworkCore/"]
RUN dotnet restore "./services/projects/host/Tasky.Projects.HttpApi.Host/Tasky.Projects.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/Tasky.Projects.HttpApi.Host"
RUN dotnet build "./Tasky.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tasky.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.Projects.HttpApi.Host.dll"]