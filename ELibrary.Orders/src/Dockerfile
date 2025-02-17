FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ELibrary.Orders.PublicApi/ELibrary.Orders.PublicApi.csproj", "ELibrary.Orders.PublicApi/"]
COPY ["ELibrary.Orders.Application/ELibrary.Orders.Application.csproj", "ELibrary.Orders.Application/"]
COPY ["ELibrary.Orders.Domain/ELibrary.Orders.Domain.csproj", "ELibrary.Orders.Domain/"]
COPY ["ELibrary.Orders.Infrastructure/ELibrary.Orders.Infrastructure.csproj", "ELibrary.Orders.Infrastructure/"]

RUN dotnet restore "ELibrary.Orders.Infrastructure/ELibrary.Orders.Infrastructure.csproj"

COPY . .

RUN dotnet publish "ELibrary.Orders.PublicApi/ELibrary.Orders.PublicApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV UserService__BaseUrl=http://192.168.178.63:8081/
ENV BookService__BaseUrl=http://192.168.178.63:8080/api/
ENV ConnectionStrings__LocalConnection="Server=192.168.178.63, 1434;Database=ELibraryOrders;User Id=sa;Password=Lorik.1234;TrustServerCertificate=True;"

EXPOSE 8080

ENTRYPOINT ["dotnet", "ELibrary.Orders.PublicApi.dll"]
