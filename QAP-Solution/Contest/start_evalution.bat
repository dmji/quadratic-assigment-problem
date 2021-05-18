@ECHO ON

SET PSScript=.\script\start.ps1

Powershell -ExecutionPolicy Bypass -Command "& '%PSScript%' 'Evalution' '%2'
EXIT /B