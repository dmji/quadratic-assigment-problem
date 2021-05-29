@ECHO ON

SET script=.\convert.ps1

Powershell -ExecutionPolicy Bypass -Command "& '%script%'
EXIT /B