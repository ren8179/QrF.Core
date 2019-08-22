FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

COPY . .

ENTRYPOINT ["dotnet", "QrF.Core.Gateway.dll"]