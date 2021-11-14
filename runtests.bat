@echo off
echo Starting Tests...
dotnet build -c Debug -v quiet
rem dotnet test --no-build -c Debug --filter TestCategory="Complexify"
dotnet test --no-build -c Debug
rem dotnet build -c Release
rem dotnet test --no-build -c Release
