@echo off
title Config
echo Starting...
set ASPNETCORE_ENVIRONMENT=Development
dotnet bin/Debug/netcoreapp2.2/QrF.Core.Config.dll --console
