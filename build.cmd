:: Optional batch file to quickly build with some defaults.
:: Alternatively, this batch file can be invoked passing msbuild parameters, like: build.cmd /v:detailed /t:Rebuild

@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION
PUSHD "%~dp0" >NUL

:: Determine if MSBuild can be located. Allows for a better error message below.
where msbuild > %TEMP%\msbuild.txt
set /p msb=<%TEMP%\msbuild.txt

IF "%msb%"=="" (
    echo Please run %~n0 from a Visual Studio Developer Command Prompt.
    exit /b -1
)

"%msb%" /v:minimal %1 %2 %3 %4 %5 %6 %7 %8 %9

POPD >NUL
ENDLOCAL
ECHO ON