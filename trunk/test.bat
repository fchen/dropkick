@echo off

::Project UppercuT - http://uppercut.googlecode.com
::No edits to this file are required - http://uppercut.pbwiki.com

if '%2' NEQ '' goto usage
if '%3' NEQ '' goto usage
if '%1' == '/?' goto usage
if '%1' == '-?' goto usage
if '%1' == '?' goto usage
if '%1' == '/help' goto usage

SET DIR=%~d0%~p0%

SET build.config.settings="%DIR%Settings\UppercuT.config"
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\_compile.build -D:build.config.settings=%build.config.settings%
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\analyzers\_test.build %1 -D:build.config.settings=%build.config.settings%
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\analyzers\_test.build open_results -D:build.config.settings=%build.config.settings%

goto bdddoc

:bdddoc

SET APP_BDDDOC="..\lib\testing\bdddoc\bdddoc.console.exe"

"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts.Custom\_bdddoc.build -D:app.bdddoc=%APP_BDDDOC% -D:build.config.settings=%build.config.settings%
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts.Custom\_bdddoc.build open_results -D:build.config.settings=%build.config.settings%

goto finish

:usage
echo.
echo Usage: test.bat
echo Usage: test.bat all - to run all tests
echo.
goto finish

:finish