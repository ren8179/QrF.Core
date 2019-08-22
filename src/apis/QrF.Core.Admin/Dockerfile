FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/apis/QrF.Core.Admin/QrF.Core.Admin.csproj", "src/apis/QrF.Core.Admin/"]
COPY ["src/libs/QrF.Core.Utils/QrF.Core.Utils.csproj", "src/libs/QrF.Core.Utils/"]
RUN dotnet restore "src/apis/QrF.Core.Admin/QrF.Core.Admin.csproj"
COPY . .
WORKDIR "/src/src/apis/QrF.Core.Admin"
RUN dotnet build "QrF.Core.Admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QrF.Core.Admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QrF.Core.Admin.dll"]