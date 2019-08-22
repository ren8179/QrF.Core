FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/apis/QrF.Core.Ids4/QrF.Core.Ids4.csproj", "src/apis/QrF.Core.Ids4/"]
COPY ["src/libs/QrF.Core.IdentityServer4.Dapper.SqlServer/QrF.Core.IdentityServer4.Dapper.SqlServer.csproj", "src/libs/QrF.Core.IdentityServer4.Dapper.SqlServer/"]
COPY ["src/libs/QrF.Core.IdentityServer4.Dapper/QrF.Core.IdentityServer4.Dapper.csproj", "src/libs/QrF.Core.IdentityServer4.Dapper/"]
COPY ["src/libs/QrF.Core.Storage/QrF.Core.Storage.csproj", "src/libs/QrF.Core.Storage/"]
COPY ["src/libs/QrF.Core.Utils/QrF.Core.Utils.csproj", "src/libs/QrF.Core.Utils/"]
RUN dotnet restore "src/apis/QrF.Core.Ids4/QrF.Core.Ids4.csproj"
COPY . .
WORKDIR "/src/src/apis/QrF.Core.Ids4"
RUN dotnet build "QrF.Core.Ids4.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QrF.Core.Ids4.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QrF.Core.Ids4.dll"]