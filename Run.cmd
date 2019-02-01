@echo off
WHERE /Q dotnet
IF %ERRORLEVEL% NEQ 0 (
ECHO dotnet core sdk was not find, please install the latest sdk at first.
@pause
start https://www.microsoft.com/net/download/windows
exit
)
if not exist "src\apis\QrF.Core.CMS\bin\Debug\netcoreapp2.2\QrF.Core.CMS.dll" (
call Build.cmd
)

cd src\apis\QrF.Core.CMS
call Run.cmd