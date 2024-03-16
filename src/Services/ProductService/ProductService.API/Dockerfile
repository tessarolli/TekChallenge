#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/AuthService/AuthService.Contracts/AuthService.Contracts.csproj", "src/Services/AuthService/AuthService.Contracts/"]
COPY ["src/Services/ProductService/ProductService.API/ProductService.API.csproj", "src/Services/ProductService/ProductService.API/"]
COPY ["src/Services/ProductService/ProductService.Application/ProductService.Application.csproj", "src/Services/ProductService/ProductService.Application/"]
COPY ["src/Services/ProductService/ProductService.Contracts/ProductService.Contracts.csproj", "src/Services/ProductService/ProductService.Contracts/"]
COPY ["src/Services/ProductService/ProductService.Domain/ProductService.Domain.csproj", "src/Services/ProductService/ProductService.Domain/"]
COPY ["src/Services/ProductService/ProductService.Infrastructure/ProductService.Infrastructure.csproj", "src/Services/ProductService/ProductService.Infrastructure/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Application/SharedDefinitions.Application.csproj", "src/SharedDefinitions/SharedDefinitions.Application/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Domain/SharedDefinitions.Domain.csproj", "src/SharedDefinitions/SharedDefinitions.Domain/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Infrastructure/SharedDefinitions.Infrastructure.csproj", "src/SharedDefinitions/SharedDefinitions.Infrastructure/"]
COPY ["src/SharedDefinitions/SharedDefinitions.Presentation/SharedDefinitions.Presentation.csproj", "src/SharedDefinitions/SharedDefinitions.Presentation/"]
RUN dotnet restore "./src/Services/ProductService/ProductService.API/./ProductService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ProductService/ProductService.API"
RUN dotnet build "./ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.API.dll"]