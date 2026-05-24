FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY global.json .
COPY Directory.Build.props .
COPY Directory.Packages.props .
COPY nuget.config .
COPY learning_ms.slnx .

COPY src/learning_ms.ServiceDefaults/learning_ms.ServiceDefaults.csproj src/learning_ms.ServiceDefaults/
COPY src/learning_ms.Web/learning_ms.Web.csproj src/learning_ms.Web/

RUN dotnet restore src/learning_ms.Web/learning_ms.Web.csproj

COPY src/learning_ms.ServiceDefaults/ src/learning_ms.ServiceDefaults/
COPY src/learning_ms.Web/ src/learning_ms.Web/

RUN dotnet publish src/learning_ms.Web/learning_ms.Web.csproj \
    --no-restore \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN groupadd --system appgroup && useradd --system --gid appgroup appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "learning_ms.Web.dll"]