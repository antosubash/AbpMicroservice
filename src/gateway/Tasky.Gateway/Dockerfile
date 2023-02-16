#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gateway/Tasky.Gateway/Tasky.Gateway.csproj", "gateway/Tasky.Gateway/"]
RUN dotnet restore "gateway/Tasky.Gateway/Tasky.Gateway.csproj"
COPY . .
WORKDIR "/src/gateway/Tasky.Gateway"
RUN dotnet build "Tasky.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.Gateway.dll"]