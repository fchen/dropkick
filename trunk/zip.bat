@echo off

::Project UppercuT - http://uppercut.googlecode.com
::No edits to this file are required - http://uppercut.pbwiki.com

SET DIR=%~d0%~p0%
call %DIR%build.bat

%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\_zip.build -D:project.name=%PROJECT_NAME%

:: Use this option if you explicitly want to set the items in _zip.build

::%DIR%lib\tools\Nant\nant.exe /f:.\BuildScripts\_update_assemblies.build