FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ELibrary.Shared/src/ELibrary.Contracts", "ELibrary.Shared/src/ELibrary.Contracts/"]

COPY ["ELibrary.Books/ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj", "ELibrary.Books/ELibrary.Books.PublicAPI/"]
COPY ["ELibrary.Books/ELibrary.Books.Application/ELibrary.Books.Application.csproj", "ELibrary.Books/ELibrary.Books.Application/"]
COPY ["ELibrary.Books/ELibrary.Books.Domain/ELibrary.Books.Domain.csproj", "ELibrary.Books/ELibrary.Books.Domain/"]
COPY ["ELibrary.Books/ELibrary.Books.Infrastructure/ELibrary.Books.Infrastructure.csproj", "ELibrary.Books/ELibrary.Books.Infrastructure/"]
COPY ["ELibrary.Books/ELibrary.Books.Contracts/ELibrary.Books.Contracts.csproj", "ELibrary.Books/ELibrary.Books.Contracts/"]

RUN dotnet restore "ELibrary.Books/ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj"

COPY . .

RUN dotnet publish "ELibrary.Books/ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV OrdersService__BaseUrl=http://localhost:8082/
ENV UsersService__BaseUrl=http://localhost:8081/
ENV ConnectionStrings__LocalConnection="Server=192.168.178.63, 1434;Database=ELibraryBooks;User Id=sa;Password=Lorik.1234;TrustServerCertificate=True;"

EXPOSE 8080

ENTRYPOINT ["dotnet", "ELibrary.Books.PublicAPI.dll"]
