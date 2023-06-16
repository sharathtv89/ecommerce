FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ./catalog/Catalog.API/*.csproj .
RUN dotnet restore

COPY ./catalog/Catalog.API/ .
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "Catalog.API.dll" ]
