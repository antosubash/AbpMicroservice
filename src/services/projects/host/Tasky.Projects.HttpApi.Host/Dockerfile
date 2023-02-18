#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/projects/host/Tasky.Projects.HttpApi.Host/Tasky.Projects.HttpApi.Host.csproj", "services/projects/host/Tasky.Projects.HttpApi.Host/"]
COPY ["services/projects/src/Tasky.Projects.Application/Tasky.Projects.Application.csproj", "services/projects/src/Tasky.Projects.Application/"]
COPY ["services/projects/src/Tasky.Projects.Domain/Tasky.Projects.Domain.csproj", "services/projects/src/Tasky.Projects.Domain/"]
COPY ["services/projects/src/Tasky.Projects.Domain.Shared/Tasky.Projects.Domain.Shared.csproj", "services/projects/src/Tasky.Projects.Domain.Shared/"]
COPY ["services/projects/src/Tasky.Projects.Application.Contracts/Tasky.Projects.Application.Contracts.csproj", "services/projects/src/Tasky.Projects.Application.Contracts/"]
COPY ["services/projects/src/Tasky.Projects.EntityFrameworkCore/Tasky.Projects.EntityFrameworkCore.csproj", "services/projects/src/Tasky.Projects.EntityFrameworkCore/"]
COPY ["services/projects/src/Tasky.Projects.HttpApi/Tasky.Projects.HttpApi.csproj", "services/projects/src/Tasky.Projects.HttpApi/"]
RUN dotnet restore "services/projects/host/Tasky.Projects.HttpApi.Host/Tasky.Projects.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/Tasky.Projects.HttpApi.Host"
RUN dotnet build "Tasky.Projects.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.Projects.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.Projects.HttpApi.Host.dll"]