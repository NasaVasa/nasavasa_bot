FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore c-sharp.nasavasa.ru.csproj --disable-parallel
RUN dotnet publish c-sharp.nasavasa.ru.csproj -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
CMD ["dotnet", "c-sharp.nasavasa.ru.dll"]