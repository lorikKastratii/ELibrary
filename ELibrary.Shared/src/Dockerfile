FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["ELibrary.Shared.Book/ELibrary.Shared.Book.csproj", "ELibrary.Shared.Book/"]

RUN dotnet restore "ELibrary.Shared.Book/ELibrary.Shared.Book.csproj"

COPY ELibrary.Shared.Book ELibrary.Shared.Book
WORKDIR "/src/ELibrary.Shared.Book"

RUN dotnet build "ELibrary.Shared.Book.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ELibrary.Shared.Book.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ELibrary.Shared.Book.dll"]
