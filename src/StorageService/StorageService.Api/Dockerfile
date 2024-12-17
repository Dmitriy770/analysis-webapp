FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Common/Common.Web/Common.Web.csproj", "Uis/Common/Common.Web/"]
COPY ["Common/Common.Logging/Common.Logging.csproj", "Uis/Common/Common.Logging/"]
COPY ["StorageService/StorageService.Api/StorageService.Api.csproj", "StorageService.Api/"]
COPY ["StorageService/StorageService.Application/StorageService.Application.csproj", "StorageService.Application/"]
COPY ["StorageService/StorageService.Domain/StorageService.Domain.csproj", "StorageService.Domain/"]
COPY ["StorageService/StorageService.Infrastructure/StorageService.Infrastructure.csproj", "StorageService.Infrastructure/"]
RUN dotnet restore "StorageService.Api/StorageService.Api.csproj"
COPY [ "StorageService", "StorageServiсe"]
COPY [ "Common", "Common"]
WORKDIR "/src/StorageService/StorageService.Api"
RUN dotnet build "StorageService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StorageService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageService.Api.dll"]
