FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderApplication.Api/OrderApplication.Api.csproj", "OrderApplication.Api/"]
COPY ["OrderApplication.Application/OrderApplication.Application.csproj", "OrderApplication.Application/"]
COPY ["OrderApplication.Infrastructure/OrderApplication.Infrastructure.csproj", "OrderApplication.Infrastructure/"]
RUN dotnet restore "OrderApplication.Api/OrderApplication.Api.csproj"
COPY . .
WORKDIR "/src/OrderApplication.Api"
RUN dotnet build "OrderApplication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrderApplication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderApplication.Api.dll"]
