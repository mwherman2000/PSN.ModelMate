@echo Full path and filename: %~f0
@echo Drive: %~d0
@echo Path: %~p0
@echo Drive and path: %~dp0
@echo Filename without extension: %~n0
@echo Filename with    extension: %~nx0
@echo Extension: %~x0
@echo Filename as given on command line: %0
@echo Filename as given on command line minus quotes: %~0
@REM Build from parts
@SETLOCAL
@SET drv=%~d0
@SET pth=%~p0
@SET fpath=%~dp0
@SET fname=%~n0
@SET ext=%~x0
@echo Simply Constructed name: %fpath%%fname%%ext%
@echo Fully  Constructed name: %drv%%pth%%fname%%ext%
@ENDLOCAL
pause

set PATH="D:\Code\Test001\PSNModelMate3\PSN.ModelMate.Schema\DatabaseBuilder\XSD2DB-0.2-bin";%PATH%
set VERSION=9

rem xsd2db.exe -f -k true -l psn-win81-001\mssqlserver2016 -name ModelMateEFModel%VERSION% -schema "%~dp0..\..\Schema\Reference ModelGood%VERSION%.xsd" -type Sql

xsd2db.exe -f -k true -l psn-w12r2-003 -name ModelMateEFModel%VERSION% -schema "%~dp0..\..\Schema\Reference ModelGood%VERSION%.xsd" -type Sql -userid sa -password P@ssword1

pause