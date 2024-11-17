FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ELibrary.Users.PublicAPI/ELibrary.Users.PublicAPI.csproj", "ELibrary.Users.PublicAPI/"]
COPY ["ELibrary.Users.Application/ELibrary.Users.Application.csproj", "ELibrary.Users.Application/"]
COPY ["ELibrary.Users.Domain/ELibrary.Users.Domain.csproj", "ELibrary.Users.Domain/"]
COPY ["ELibrary.Users.Infrastructure/ELibrary.Users.Infrastructure.csproj", "ELibrary.Users.Infrastructure/"]

RUN dotnet restore "ELibrary.Users.PublicAPI/ELibrary.Users.PublicAPI.csproj"

COPY . .

RUN dotnet publish "ELibrary.Users.PublicAPI/ELibrary.Users.PublicAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV OrdersService__BaseUrl=http://localhost:8082/
ENV BookService__BaseUrl=http://localhost:8080/api/
ENV ConnectionStrings__LocalConnection="Server=192.168.178.63, 1434;Database=ELibraryUsers;User Id=sa;Password=Lorik.1234;TrustServerCertificate=True;"

EXPOSE 8080

ENTRYPOINT ["dotnet", "ELibrary.Users.PublicAPI.dll"]
