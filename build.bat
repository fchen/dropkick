@echo off

::Project UppercuT - http://uppercut.googlecode.com
::No edits to this file are required - http://uppercut.pbwiki.com

SET DIR=%~d0%~p0%
call %DIR%Settings\build.settings.bat

if '%1' NEQ '' goto usage
if '%2' NEQ '' goto usage
if '%3' NEQ '' goto usage
if '%1' == '/?' goto usage
if '%1' == '-?' goto usage
if '%1' == '?' goto usage
if '%1' == '/help' goto usage

%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\__master.build -D:project.name=%PROJECT_NAME% -D:repository.path=%REPO_PATH% -D:dlls.test=%NCOVER_TEST_DLLS% -D:dirs.db.project=%DB_PROJECT_FOLDER% -D:dirs.report.project=%REPORT_PROJECT_FOLDER% -D:version.major=%VERSION_MAJOR% -D:version.minor=%VERSION_MINOR% -D:company.name=%COMPANY_NAME% -D:microsoft.framework=%MICROSOFT_FRAMEWORK% -D:language.short=%PROJECT_LANGUAGE% -D:app.ncover.tester=%APP_TEST_FOR_NCOVER% -D:app.ncover=%APP_NCOVER% -D:app.ncover_explorer=%APP_NCOVER_EXPLORER% -D:app.ndepend=%APP_NDEPEND% -D:allow.partially_trusted_callers=%ALLOW_PARTIALLY_TRUSTED_CALLERS% -D:test.framework=%TESTING_FRAMEWORK% -D:source_control_type=%SOURCE_CONTROL_TYPE%

:: Use this option if you explicitly want to set the items in __master.build, _compile.build, _ncover.build, and _package.build

::%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\__master.build

goto finish

:usage
echo.
echo Usage: build.bat
echo Ensure you've set project name (%PROJECT_NAME%), your repository path (%REPO_PATH%), and company name (%COMPANY_NAME%).
echo.
goto finish

:finish