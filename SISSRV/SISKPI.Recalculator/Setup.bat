@echo off
set CURRENT_DIR=%cd%
installutil  %CURRENT_DIR%\SISKPI.Recalculator.exe
net start SISKPI.Recalculator

