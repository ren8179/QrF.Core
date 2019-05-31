@ echo off
set _name=丝路智旅-配置中心API
set _nod=QrF.Core.Config

@ echo off
echo 正在启用管理员权限... 
%1 %2
ver|find "5.">nul&&goto :st
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :st","","runas",1)(window.close)&goto :eof
 
:st
copy "%~0" "%windir%\system32\"
echo 启用管理员权限成功
echo=
echo 准备卸载服务
echo=
echo 停止已安装的%_name%服务
sc stop %_nod%
choice /t 2 /d y /n >nul
echo=
echo 删除已安装的%_name%服务
sc delete %_nod%
echo=
echo 卸载结束
echo. & pause 