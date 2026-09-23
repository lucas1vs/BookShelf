FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY BookShelf.API/BookShelf.API.csproj BookShelf.API/
COPY BookShelf.Domain/BookShelf.Domain.csproj BookShelf.Domain/
COPY BookShelf.EFCore/BookShelf.EFCore.csproj BookShelf.EFCore/
RUN dotnet restore BookShelf.API/BookShelf.API.csproj

COPY . . 
RUN dotnet publish BookShelf.API/BookShelf.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BookShelf.API.dll"]