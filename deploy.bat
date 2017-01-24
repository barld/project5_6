taskkill /F /IM Webshop.exe


SET dest=%~dp0productionEnv

if not exist "%dest%" mkdir "%dest%"
xcopy /Y /s "%~dp0bin\Debug" "%dest%"

SET webDest="%dest%/webContent"

if not exist %webDest% mkdir %webDest%
xcopy /Y /s "%~dp0webContent\web" %webDest%

copy /Y "%~dp0startApp.bat" "%dest%/startApp.bat"
START "" "%dest%/startApp.bat"

