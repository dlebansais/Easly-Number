@echo off
rem goto upload

if not exist "..\Misc-Beta-Test\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" goto error_console1
if not exist "..\Misc-Beta-Test\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe" goto error_console2
if not exist "..\Misc-Beta-Test\Test-Easly-Number\bin\x64\Debug\Test-Easly-Number.dll" goto error_not_built
if not exist "..\Misc-Beta-Test\Test-Easly-Number\bin\x64\Release\Test-Easly-Number.dll" goto error_not_built
if exist *.log del *.log
if exist ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Debug\Coverage-Easly-Number-Debug_coverage.xml del ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Debug\Coverage-Easly-Number-Debug_coverage.xml
if exist ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Release\Coverage-Easly-Number-Release_coverage.xml del ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Release\Coverage-Easly-Number-Release_coverage.xml

:runtests
"..\Misc-Beta-Test\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" -register:user -target:"..\Misc-Beta-Test\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe" -targetargs:"..\Misc-Beta-Test\Test-Easly-Number\bin\x64\Debug\Test-Easly-Number.dll --trace=Debug --labels=All --where=cat==Coverage" -filter:"+[Easly-Number*]* -[Test-Easly-Number*]*" -output:"..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Debug\Coverage-Easly-Number-Debug_coverage.xml"
"..\Misc-Beta-Test\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" -register:user -target:"..\Misc-Beta-Test\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe" -targetargs:"..\Misc-Beta-Test\Test-Easly-Number\bin\x64\Release\Test-Easly-Number.dll --trace=Debug --labels=All --where=cat==Coverage" -filter:"+[Easly-Number*]* -[Test-Easly-Number*]*" -output:"..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Release\Coverage-Easly-Number-Release_coverage.xml"

:upload
if exist ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Debug\Coverage-Easly-Number-Debug_coverage.xml ..\Misc-Beta-Test\packages\Codecov.1.1.1\tools\codecov -v -f "..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Debug\Coverage-Easly-Number-Debug_coverage.xml" -t "8ac8c077-35e0-4ccd-b327-08936fd9f0fe"
ECHO Waiting 30 seconds
PING -n 30 -w 1000 127.1 > NUL
if exist ..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Release\Coverage-Easly-Number-Release_coverage.xml ..\Misc-Beta-Test\packages\Codecov.1.1.1\tools\codecov -v -f "..\Misc-Beta-Test\Test-Easly-Number\obj\x64\Release\Coverage-Easly-Number-Release_coverage.xml" -t "8ac8c077-35e0-4ccd-b327-08936fd9f0fe"
goto end

:error_console1
echo ERROR: OpenCover.Console not found.
goto end

:error_console2
echo ERROR: nunit3-console not found.
goto end

:error_not_built
echo ERROR: Test-Easly-Number.dll not built (both Debug and Release are required).
goto end

:end
