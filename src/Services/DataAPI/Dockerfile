FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["MasterApp.sln", "MasterApp.sln"]


COPY ["Services/DataAPI/DataAPI.csproj", "Services/DataAPI/"]

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "MasterApp.sln"

COPY . .
WORKDIR "/src/Services/DataAPI"
RUN dotnet build "DataAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataAPI.dll"]
