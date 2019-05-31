@ echo off 
set _dic=%~dp0
set _name=丝路智旅-配置中心API
set _nod=QrF.Core.Config
set _des=丝路智旅-配置中心API

@ echo off
echo 正在启用管理员权限... 
%1 %2
ver|find "5.">nul&&goto :st
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :st","","runas",1)(window.close)&goto :eof
 
:st
copy "%~0" "%windir%\system32\"
echo 启用管理员权限成功
echo=
echo 开始安装……
echo=
echo 停止已安装的%_name%服务
sc stop %_nod%
choice /t 2 /d y /n >nul
echo=
echo 删除已安装的%_name%服务
sc delete %_nod%
echo=
echo 安装%_name%服务……
choice /t 2 /d y /n >nul
sc create %_nod% binPath= "%_dic%\%_nod%.exe" DisplayName= "%_name%" start= auto
choice /t 1 /d y /n >nul
echo=
sc description %_nod% "%_des%"
echo=
echo 启动%_name%服务……
sc start %_nod%
echo=
echo 查询%_name%服务状态……
echo ===== STATE : 1 已停止
echo ===== STATE : 2 正在启动
echo ===== STATE : 3 正在停止
echo ===== STATE : 4 正在运行
echo=
echo 当前状态:
choice /t 2 /d y /n >nul
sc query %_nod%
echo=
echo 安装结束
echo. & pause 