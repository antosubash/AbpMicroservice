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
COPY ["gateway/Tasky.Gateway/Tasky.Gateway.csproj", "gateway/Tasky.Gateway/"]
COPY ["shared/Tasky.ServiceDefaults/Tasky.ServiceDefaults.csproj", "shared/Tasky.ServiceDefaults/"]
COPY ["shared/Tasky.Shared/Tasky.Shared.csproj", "shared/Tasky.Shared/"]
RUN dotnet restore "./gateway/Tasky.Gateway/Tasky.Gateway.csproj"
COPY . .
WORKDIR "/src/gateway/Tasky.Gateway"
RUN dotnet build "./Tasky.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tasky.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.Gateway.dll"]