@echo off

::Project UppercuT - http://uppercut.googlecode.com
::Learn how to edit this file at http://uppercut.pbwiki.com

::project variables
SET PROJECT_NAME="dropkick"
SET REPO_PATH="http://dropkick.googlecode.com/svn/"
SET DB_PROJECT_FOLDER="db"
SET REPORT_PROJECT_FOLDER="reports"

::framework
SET MICROSOFT_FRAMEWORK="net-3.5"
::valid values are net-1.1,net-2.0,net-3.5 
SET PROJECT_LANGUAGE="cs"
::valid values are vb,cs
SET TESTING_FRAMEWORK="mbunit"
::valid values are mbunit, nunit
SET SOURCE_CONTROL_TYPE="svn"
::valid values are svn, vss, tfs, vault, git

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
SET APP_TEST_FOR_NCOVER="..\..\lib\references\MbUnit\MbUnit.Cons.exe.NO"
::SET APP_TEST_FOR_NCOVER="..\..\lib\references\NUnit\nunit-console.exe.NO"
SET APP_NDEPEND="C:\Program Files\NDepend\NDepend.Console.exe.NO"