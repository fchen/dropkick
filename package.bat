@echo off

::Project UppercuT - http://uppercut.googlecode.com
::No edits to this file are required - http://uppercut.pbwiki.com

SET DIR=%~d0%~p0%
call %DIR%Settings\build.settings.bat

%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\_package.build -D:db.proj.name=%DB_PROJECT_FOLDER% -D:report.proj.name=%REPORT_PROJECT_FOLDER%

:: Use this option if you explicitly want to set the items in the _package.build

::%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\_package.build