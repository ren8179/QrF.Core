@ echo off 
set _dic=%~dp0
set _name=˿·����-��������API
set _nod=QrF.Core.Config
set _des=˿·����-��������API

@ echo off
echo �������ù���ԱȨ��... 
%1 %2
ver|find "5.">nul&&goto :st
mshta vbscript:createobject("shell.application").shellexecute("%~s0","goto :st","","runas",1)(window.close)&goto :eof
 
:st
copy "%~0" "%windir%\system32\"
echo ���ù���ԱȨ�޳ɹ�
echo=
echo ��ʼ��װ����
echo=
echo ֹͣ�Ѱ�װ��%_name%����
sc stop %_nod%
choice /t 2 /d y /n >nul
echo=
echo ɾ���Ѱ�װ��%_name%����
sc delete %_nod%
echo=
echo ��װ%_name%���񡭡�
choice /t 2 /d y /n >nul
sc create %_nod% binPath= "%_dic%\%_nod%.exe" DisplayName= "%_name%" start= auto
choice /t 1 /d y /n >nul
echo=
sc description %_nod% "%_des%"
echo=
echo ����%_name%���񡭡�
sc start %_nod%
echo=
echo ��ѯ%_name%����״̬����
echo ===== STATE : 1 ��ֹͣ
echo ===== STATE : 2 ��������
echo ===== STATE : 3 ����ֹͣ
echo ===== STATE : 4 ��������
echo=
echo ��ǰ״̬:
choice /t 2 /d y /n >nul
sc query %_nod%
echo=
echo ��װ����
echo. & pause 