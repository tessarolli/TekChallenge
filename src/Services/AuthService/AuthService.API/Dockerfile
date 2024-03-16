#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/AuthService/AuthService.API/AuthService.API.csproj", "src/Services/AuthService/AuthService.API/"]
COPY ["src/Services/AuthService/AuthService.Contracts/AuthService.Contracts.csproj", "src/Services/AuthService/AuthService.Contracts/"]
COPY ["src/Services/AuthService/AuthService.Application/AuthService.Application.csproj", "src/Services/AuthService/AuthService.Application/"]
COPY ["src/Services/AuthService/AuthService.Domain/AuthService.Domain.csproj", "src/Services/AuthService/AuthService.Domain/"]
COPY ["src/Services/AuthService/AuthService.Infrastructure/AuthService.Infrastructure.csproj", "src/Services/AuthService/AuthService.Infrastructure/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Presentation/SharedDefinitions.Presentation.csproj", "src/SharedDefinitions/SharedDefinitions.Presentation/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Application/SharedDefinitions.Application.csproj", "src/SharedDefinitions/SharedDefinitions.Application/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Domain/SharedDefinitions.Domain.csproj", "src/SharedDefinitions/SharedDefinitions.Domain/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Infrastructure/SharedDefinitions.Infrastructure.csproj", "src/SharedDefinitions/SharedDefinitions.Infrastructure/"]
RUN dotnet restore "./src/Services/AuthService/AuthService.API/./AuthService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/AuthService/AuthService.API"
RUN dotnet build "./AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]