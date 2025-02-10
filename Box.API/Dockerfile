# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Box.API/Box.API.csproj", "Box.API/"]
COPY ["Box.Applicaton/Box.Applicaton.csproj", "Box.Applicaton/"]
COPY ["Box.Domain/Box.Domain.csproj", "Box.Domain/"]
COPY ["Box.Infrastructure.DataAccess/Box.Infrastructure.DataAccess.csproj", "Box.Infrastructure.DataAccess/"]
COPY ["Box.Contracts/Box.Contracts.csproj", "Box.Contracts/"]
COPY ["Box.Infrastructure/Box.Infrastructure.csproj", "Box.Infrastructure/"]
RUN dotnet restore "./Box.API/Box.API.csproj"
COPY . .
WORKDIR "/src/Box.API"
RUN dotnet build "./Box.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Box.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Box.API.dll"]