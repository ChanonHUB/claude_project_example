@echo off
echo ========================================
echo Adding Firewall Rule for .NET Backend
echo ========================================
echo.
echo This will allow mobile devices to access the backend API on port 5104
echo You need to run this as Administrator
echo.
pause

netsh advfirewall firewall add rule name="Mobile App Backend API" dir=in action=allow protocol=TCP localport=5104

echo.
echo ========================================
echo Firewall rule added successfully!
echo ========================================
echo.
pause
