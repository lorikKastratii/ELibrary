FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution and projects
COPY ["ELibrary.Consumers.ElasticService/ELibrary.Consumers.ElasticService.csproj", "ELibrary.Consumers.ElasticService/"]

# Restore dependencies
RUN dotnet restore "ELibrary.Consumers.ElasticService/ELibrary.Consumers.ElasticService.csproj"

# Copy full project source
COPY ELibrary.Consumers.ElasticService ELibrary.Consumers.ElasticService

WORKDIR "/src/ELibrary.Consumers.ElasticService"
RUN dotnet build "ELibrary.Consumers.ElasticService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ELibrary.Consumers.ElasticService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ELibrary.Consumers.ElasticService.dll"]
