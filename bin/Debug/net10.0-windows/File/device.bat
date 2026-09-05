@echo off
"%~dp0\..\platform-tools\adb.exe" kill-server
"%~dp0\..\platform-tools\adb.exe" start-server
echo ADB restarted successfully.
pause