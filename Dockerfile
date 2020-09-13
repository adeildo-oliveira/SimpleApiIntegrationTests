FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SimpleApiIntegrationTests/SimpleApiIntegrationTests.csproj", "SimpleApiIntegrationTests/"]
RUN dotnet restore "SimpleApiIntegrationTests/SimpleApiIntegrationTests.csproj"
COPY . .
WORKDIR "/src/SimpleApiIntegrationTests"

FROM build AS publish
RUN dotnet publish "SimpleApiIntegrationTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleApiIntegrationTests.dll"]