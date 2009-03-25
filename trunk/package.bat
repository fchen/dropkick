@echo off

::Project UppercuT - http://uppercut.googlecode.com
::Learn how to edit this file at http://uppercut.pbwiki.com

:: Use this option if you set the items here - EASIER TO UPGRADE TO NEWER BUILD SCRIPTS

SET DB_PROJECT_FOLDER="db"
SET REPORT_PROJECT_FOLDER="reports"

.\lib\tools\Nant\nant.exe /f:.\BuildScripts\_package.build -D:db.proj.name=%DB_PROJECT_FOLDER% -D:report.proj.name=%REPORT_PROJECT_FOLDER%

:: Use this option if you explicitly want to set the items in the _package.build

::.\lib\tools\Nant\nant.exe /f:.\BuildScripts\_package.build