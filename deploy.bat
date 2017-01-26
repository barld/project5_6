Taskkill /F /IM Webshop.exe

If Not Exist "%~dp0startApp.bat" (
    Echo= startApp.bat file not found
    GoTo Next
)

Rem %~dp0 means the current folder where this .bat is executed from
Set "dest=%~dp0productionEnv"
Set "webDest=%dest%\webContent"

If Not Exist "%dest%" MD "%dest%"
XCopy "%~dp0Webshop\bin\Debug" "%dest%" /Y /S
If Not Exist "%webDest%" MD "%webDest%"
XCopy "%~dp0Webshop\webContent\web" "%webDest%" /Y /S
Copy /Y "%~dp0startApp.bat" "%dest%"
Call "%dest%\startApp.bat"
Del "%~dp0startApp.bat"
Echo= Deleted startApp.bat

:Next