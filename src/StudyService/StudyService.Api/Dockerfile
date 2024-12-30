FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudyService/StudyService.Api/StudyService.Api.csproj", "StudyService.Api/"]
COPY ["StudyService/StudyService.Application/StudyService.Application.csproj", "StudyService.Application/"]
COPY ["StudyService/StudyService.Domain/StudyService.Domain.csproj", "StudyService.Domain/"]
COPY ["Common/Common.Web/Common.Web.csproj", "../Common/Common.Web/"]
COPY ["Common/Common.Logging/Common.Logging.csproj", "../Common/Common.Logging/"]
RUN dotnet restore "StudyService.Api/StudyService.Api.csproj"
COPY ["StudyService", "StudyService"]
COPY ["Common", "Common"]
WORKDIR "/src/StudyService/StudyService.Api"
RUN dotnet build "StudyService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StudyService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyService.Api.dll"]
