@ echo off
set _name=˿·����-��������API
set _nod=QrF.Core.Config

@ echo off
echo �������ù���ԱȨ��... 
%1 %2
ver|find "5.">nul&&goto :st
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :st","","runas",1)(window.close)&goto :eof
 
:st
copy "%~0" "%windir%\system32\"
echo ���ù���ԱȨ�޳ɹ�
echo=
echo ׼��ж�ط���
echo=
echo ֹͣ�Ѱ�װ��%_name%����
sc stop %_nod%
choice /t 2 /d y /n >nul
echo=
echo ɾ���Ѱ�װ��%_name%����
sc delete %_nod%
echo=
echo ж�ؽ���
echo. & pause 