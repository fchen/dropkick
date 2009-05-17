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
"%DIR%lib\tools\Nant\nant.exe" %1 /f:.\BuildScripts\_compile.build -D:build.config.settings=%build.config.settings%

:: DEPRECATED
:: call "%DIR%Settings\build.settings.bat"

::"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\_compile.build -D:project.name=%PROJECT_NAME% -D:microsoft.framework=%MICROSOFT_FRAMEWORK%

:: Use this option if you explicitly want to set the items in _compile.build
::"%DIR%libs\tools\Nant\nant.exe" /f:.\BuildScripts\_compile.build

if '%TESTING_FRAMEWORK%' == '"mbunit"' goto mbunit
if '%TESTING_FRAMEWORK%' == '"nunit"' goto nunit

:mbunit
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\Analyzers\_MbUnit.build %1 -D:build.config.settings=%build.config.settings%
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\Analyzers\_MbUnit.build open_results -D:build.config.settings=%build.config.settings%
goto bdddoc

:bdddoc

SET APP_BDDDOC="..\lib\testing\bdddoc\bdddoc.console.exe"

%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts.Custom\_bdddoc.build -D:app.bdddoc=%APP_BDDDOC% -D:build.config.settings=%build.config.settings%
%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts.Custom\_bdddoc.build open_results -D:build.config.settings=%build.config.settings%

goto finish

:nunit
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\Analyzers\_nunit.build %1 -D:build.config.settings=%build.config.settings%
"%DIR%lib\tools\Nant\nant.exe" /f:.\BuildScripts\Analyzers\_nunit.build open_results -D:build.config.settings=%build.config.settings%
goto finish

:usage
echo.
echo Usage: test.bat
echo Usage: test.bat run_all_tests to run all tests
echo.
goto finish

:finish