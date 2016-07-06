@ECHO off
REM Replace the below path of exe file to the right one on your pc
SET SERVICE_EXE_PATH="E:\ProgrammingExample\QuartzScheduler_Windows Services.Net\QuartzScheduler_Windows Services.Net\bin\Debug\QuartzScheduler_Windows Services.Net.exe"

REM the following directory is for .NET version
SET INSTALL_UTIL_HOME=%WINDIR%"\Microsoft.NET\Framework\v"
SET SECONDPART="\InstallUtil.exe"

REM Check the existence of .NET 4.0.30319
SET DOTNETVER=4.0.30319
  IF EXIST %INSTALL_UTIL_HOME%%DOTNETVER%%SECONDPART% GOTO install
GOTO fail
:install
  ECHO Found .NET Framework version %DOTNETVER%
  ECHO Installing Birst service [%SERVICE_EXE_PATH%]...  
  %INSTALL_UTIL_HOME%%DOTNETVER%%SECONDPART% %SERVICE_EXE_PATH%
  NET start ChienService
  REM services.msc
  GOTO end
:fail
  echo FAILURE -- Could not find .NET Framework install
:param_error
  echo USAGE: InstallService.bat [install type (I or U)] [application (.exe)]
:end
  ECHO DONE!!!
  PAUSE
