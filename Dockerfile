FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY . /app

RUN dotnet publish --configuration Release --property:PublishDir=bin

FROM alpine as final
WORKDIR /app
COPY --from=build /app/bin /app/bin

CMD ["cp",  "/app/bin/jellyfin-sort-by-raw-name-plugin.dll", "/out/jellyfin-sort-by-raw-name-plugin.dll"]