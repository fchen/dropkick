@echo off

::Project UppercuT - http://uppercut.googlecode.com
::Learn how to edit this file at http://uppercut.pbwiki.com

:: Use this option if you set the items here - EASIER TO UPGRADE TO NEWER BUILD SCRIPTS

::project variables
SET PROJECT_NAME="dropkick"
SET SVN_REPO_PATH="http://dropkick.googlecode.com/svn/"
SET DB_PROJECT_FOLDER="db"
SET REPORT_PROJECT_FOLDER="reports"

::framework
SET MICROSOFT_FRAMEWORK="net-3.5"
::valid values are net-1.1,net-2.0,net-3.5 
SET PROJECT_LANGUAGE="cs"
::valid values are vb,cs

::ncover specific
SET NCOVER_TEST_DLLS=""

:: assembly variables
SET VERSION_MAJOR="0"
SET VERSION_MINOR="0"
SET COMPANY_NAME="Fervent Coder Software and ACuriousMind Software"
SET ALLOW_PARTIALLY_TRUSTED_CALLERS="false"
::partially trusted callers doesn't set correctly with assembly generator

::external tool locations
::  -to not use the tool, change to a location that doesn't exist
SET APP_NCOVER="..\..\lib\tools\NCover\NCover.Console.exe.NO"
SET APP_NCOVER_EXPLORER="..\..\lib\tools\NCover\NCoverExplorer.Console.exe.NO"
SET APP_MBUNIT_FOR_NCOVER="..\..\lib\references\MbUnit\MbUnit.Cons.exe.NO"
SET APP_NDEPEND="C:\Program Files\NDepend\NDepend.Console.exe.NO"


.\lib\tools\Nant\nant.exe /f:.\BuildScripts\__master.build -D:project.name=%PROJECT_NAME% -D:repository.path=%SVN_REPO_PATH% -D:dlls.test=%NCOVER_TEST_DLLS% -D:dirs.db.project=%DB_PROJECT_FOLDER% -D:dirs.report.project=%REPORT_PROJECT_FOLDER% -D:version.major=%VERSION_MAJOR% -D:version.minor=%VERSION_MINOR% -D:company.name=%COMPANY_NAME% -D:microsoft.framework=%MICROSOFT_FRAMEWORK% -D:language.short=%PROJECT_LANGUAGE% -D:app.mbunit=%APP_MBUNIT_FOR_NCOVER% -D:app.ncover=%APP_NCOVER% -D:app.ncover_explorer=%APP_NCOVER_EXPLORER% -D:app.ndepend=%APP_NDEPEND% -D:allow.partially_trusted_callers=%ALLOW_PARTIALLY_TRUSTED_CALLERS%

:: Use this option if you explicitly want to set the items in __master.build, _compile.build, _ncover.build, and _package.build

::.\lib\tools\Nant\nant.exe /f:.\BuildScripts\__master.build