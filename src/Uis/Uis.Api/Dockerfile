FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Uis.Api/Uis.Api.csproj", "Uis.Api/"]
COPY ["Uis.Infrastructure/Uis.Infrastructure.csproj", "Uis.Infrastructure/"]
COPY ["Uis.Application/Uis.Application.csproj", "Uis.Application/"]
COPY ["Uis.Domain/Uis.Domain.csproj", "Uis.Domain/"]
COPY ["Common/Common.Configuration/Common.Configuration.csproj", "Common/Common.Configuration/"]
COPY ["Common/Common.Logging/Common.Logging.csproj", "Common/Common.Logging/"]
COPY ["Common/Common.Web/Common.Web.csproj", "Common/Common.Web/"]
RUN dotnet restore "Uis.Api/Uis.Api.csproj"
COPY . .
WORKDIR "/src/Uis.Api"
RUN dotnet build "Uis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Uis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uis.Api.dll"]
