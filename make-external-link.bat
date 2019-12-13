set dir=%~p1
echo %dir%
mklink/h "%~dp0\References\CitizenFX.Core.dll" "%~1"
mklink/h "%~dp0\References\System.Drawing.dll" "%dir%\System.Drawing.dll"