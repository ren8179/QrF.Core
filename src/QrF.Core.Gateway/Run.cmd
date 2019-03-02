@echo off
title Gateway
echo Starting...
set ASPNETCORE_ENVIRONMENT=Development
dotnet bin/Debug/netcoreapp2.2/QrF.Core.Gateway.dll --console