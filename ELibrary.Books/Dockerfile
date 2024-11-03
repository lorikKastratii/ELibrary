FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj", "ELibrary.Books.PublicAPI/"]
COPY ["ELibrary.Books.Application/ELibrary.Books.Application.csproj", "ELibrary.Books.Application/"]
COPY ["ELibrary.Books.Domain/ELibrary.Books.Domain.csproj", "ELibrary.Books.Domain/"]
COPY ["ELibrary.Books.Infrastructure/ELibrary.Books.Infrastructure.csproj", "ELibrary.Books.Infrastructure/"]
COPY ["ELibrary.Books.Contracts/ELibrary.Books.Contracts.csproj", "ELibrary.Books.Contracts/"]

RUN dotnet restore "ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj"

COPY . .

RUN dotnet publish "ELibrary.Books.PublicAPI/ELibrary.Books.PublicAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ELibrary.Books.PublicAPI.dll"]
