FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ELibrary.Orders.Application/ELibrary.Orders.Application.csproj", "CleanArchitecture.Api/"]
COPY ["src/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "CleanArchitecture.Application/"]
COPY ["src/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "CleanArchitecture.Domain/"]
COPY ["src/CleanArchitecture.Contracts/CleanArchitecture.Contracts.csproj", "CleanArchitecture.Contracts/"]
COPY ["src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "CleanArchitecture.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . ../
WORKDIR /src/CleanArchitecture.Api
RUN dotnet build "CleanArchitecture.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]


-----

# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file and project files for dependency restoration
COPY ["src/ELibrary.Orders.sln", "./"]
COPY ["src/ELibrary.Orders.Application/ELibrary.Orders.Application.csproj", "ELibrary.Orders.Application/"]
COPY ["src/ELibrary.Orders.Domain/ELibrary.Orders.Domain.csproj", "ELibrary.Orders.Domain/"]
COPY ["src/ELibrary.Orders.Infrastructure/ELibrary.Orders.Infrastructure.csproj", "ELibrary.Orders.Infrastructure/"]
COPY ["src/ELibrary.Orders.PublicApi/ELibrary.Orders.PublicApi.csproj", "ELibrary.Orders.PublicApi/"]
RUN dotnet restore "ELibrary.Orders.PublicApi/ELibrary.Orders.PublicApi.csproj"

# Copy the entire source code and build the application
COPY . ./
WORKDIR /app/ELibrary.Orders.PublicApi
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/ELibrary.Orders.PublicApi/out .

# Expose the port the app runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "ELibrary.Orders.PublicApi.dll"]
