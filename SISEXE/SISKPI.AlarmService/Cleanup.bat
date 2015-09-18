@echo off
net stop SISKPI.AlarmService
set CURRENT_DIR=%cd%
installutil /u %CURRENT_DIR%\SISKPI.AlarmService.exe
