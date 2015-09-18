@echo off
set CURRENT_DIR=%cd%
installutil  %CURRENT_DIR%\SISKPI.AlarmService.exe
net start SISKPI.AlarmService

