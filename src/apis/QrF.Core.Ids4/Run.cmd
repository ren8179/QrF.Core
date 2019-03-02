@echo off
title Ids4
echo Starting...
set ASPNETCORE_ENVIRONMENT=Development
set ASPNETCORE_URLS=http://localhost:6666
dotnet bin/Debug/netcoreapp2.2/QrF.Core.Ids4.dll --console
