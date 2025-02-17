FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["ELibrary.Shared.Book/ELibrary.Shared.Book.csproj", "ELibrary.Shared.Book/"]

RUN dotnet restore "ELibrary.Shared.Book/ELibrary.Shared.Book.csproj"

COPY ..

RUN dotnet publish "ELibrary.Shared.Book/ELibrary.Shared.Book.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ELibrary.Shared.Book.dll"]
