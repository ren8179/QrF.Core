@echo off
title CMS
echo Starting...
set ASPNETCORE_ENVIRONMENT=Development
set ASPNETCORE_URLS=http://localhost:5600
dotnet bin/Debug/netcoreapp2.2/QrF.Core.CMS.dll