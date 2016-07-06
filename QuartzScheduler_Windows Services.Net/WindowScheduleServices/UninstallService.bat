@ECHO off
REM Enter the folder contains setup files
SET SERVICE_EXE_PATH="E:\ProgrammingExample\QuartzScheduler_Windows Services.Net\QuartzScheduler_Windows Services.Net\bin\Debug\QuartzScheduler_Windows Services.Net.exe"

REM the following directory is for .NET version
SET INSTALL_UTIL_HOME=%WINDIR%"\Microsoft.NET\Framework\v"
SET SECONDPART="\InstallUtil.exe"

REM Check the existence of .NET 4.0.30319
SET DOTNETVER=4.0.30319
  IF EXIST %INSTALL_UTIL_HOME%%DOTNETVER%%SECONDPART% GOTO uninstall
GOTO fail
:uninstall
  ECHO Found .NET Framework version %DOTNETVER%
  ECHO Unintalling Chien's service [%SERVICE_EXE_PATH%]...  
  %INSTALL_UTIL_HOME%%DOTNETVER%%SECONDPART% /u %SERVICE_EXE_PATH%  
  GOTO end
:fail
  echo FAILURE -- Could not find .NET Framework uninstall
:param_error
  echo USAGE: UninstallService.bat [install type (I or U)] [application (.exe)]
:end
  ECHO DONE!!!
  PAUSE
