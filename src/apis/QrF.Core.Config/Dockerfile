FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/apis/QrF.Core.Config/QrF.Core.Config.csproj", "src/apis/QrF.Core.Config/"]
COPY ["src/libs/QrF.Core.Utils/QrF.Core.Utils.csproj", "src/libs/QrF.Core.Utils/"]
RUN dotnet restore "src/apis/QrF.Core.Config/QrF.Core.Config.csproj"
COPY . .
WORKDIR "/src/src/apis/QrF.Core.Config"
RUN dotnet build "QrF.Core.Config.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QrF.Core.Config.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QrF.Core.Config.dll"]