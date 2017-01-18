SET dest=%~dp0../test

if not exist "%dest%" mkdir "%dest%"
xcopy /s "%~dp0bin\Debug" "%dest%"

SET webDest="%dest%/webContent"

if not exist %webDest% mkdir %webDest%
xcopy /s "%~dp0webContent\web" %webDest%


copy "%~dp0startApp.bat" "%dest%/startApp.bat"
START "" "%dest%/startApp.bat"

