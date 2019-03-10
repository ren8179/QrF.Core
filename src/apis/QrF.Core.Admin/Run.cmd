@echo off
title Admin
echo Starting...
set ASPNETCORE_ENVIRONMENT=Development
dotnet bin/Debug/netcoreapp2.2/QrF.Core.Admin.dll --console
