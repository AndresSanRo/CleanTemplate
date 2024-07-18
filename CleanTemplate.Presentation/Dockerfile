#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "/"]
COPY ["Directory.Build.props", "/"]
COPY ["nuget.config", "/"]
COPY ["CleanTemplate.sln", "./"]
COPY ["CleanTemplate.Presentation/CleanTemplate.Presentation.csproj", "CleanTemplate.Presentation/"]
COPY ["CleanTemplate.Core/CleanTemplate.Core.csproj", "CleanTemplate.Core/"]
COPY ["CleanTemplate.Infrastructure/CleanTemplate.Infrastructure.csproj", "CleanTemplate.Infrastructure/"]

RUN dotnet restore "./CleanTemplate.Presentation/CleanTemplate.Presentation.csproj" --configfile "/nuget.config"
COPY . .
WORKDIR "/src/CleanTemplate.Presentation"
RUN dotnet build "CleanTemplate.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CleanTemplate.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanTemplate.Presentation.dll"]