FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/QrF.Core.Gateway/QrF.Core.Gateway.csproj", "src/QrF.Core.Gateway/"]
COPY ["src/libs/QrF.Core.GatewayExtension.Dapper.SqlServer/QrF.Core.GatewayExtension.Dapper.SqlServer.csproj", "src/libs/QrF.Core.GatewayExtension.Dapper.SqlServer/"]
COPY ["src/libs/QrF.Core.GatewayExtension/QrF.Core.GatewayExtension.csproj", "src/libs/QrF.Core.GatewayExtension/"]
COPY ["src/libs/QrF.Core.Utils/QrF.Core.Utils.csproj", "src/libs/QrF.Core.Utils/"]
RUN dotnet restore "src/QrF.Core.Gateway/QrF.Core.Gateway.csproj"
COPY . .
WORKDIR "/src/src/QrF.Core.Gateway"
RUN dotnet build "QrF.Core.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QrF.Core.Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QrF.Core.Gateway.dll"]