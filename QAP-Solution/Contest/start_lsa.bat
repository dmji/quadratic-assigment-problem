@ECHO ON

SET PSScript=.\script\start.ps1
SET Count = 1

Powershell -ExecutionPolicy Bypass -Command "& '%PSScript%' 'LSA' '%Count%'
EXIT /B