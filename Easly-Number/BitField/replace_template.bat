rem echo Replacing in folder '%~1'

powershell -Command "(gc %~1\BitField_Template.cstext) -replace 'Template', 'uint' | Out-File -encoding UTF8 %~1\BitField_uint.cs.temp"
if not exist "%~1\BitField_uint.cs" goto copy_uint
fc /B "%~1\BitField_uint.cs.temp" "%~1\BitField_uint.cs" > nul
if ERRORLEVEL 1 goto copy_uint
goto ignore_uint
:copy_uint
copy "%~1\BitField_uint.cs.temp" "%~1\BitField_uint.cs" > nul
:ignore_uint
del "%~1\BitField_uint.cs.temp"

powershell -Command "(gc %~1\BitField_Template.cstext) -replace 'Template', 'byte' | Out-File -encoding UTF8 %~1\BitField_byte.cs.temp"
if not exist "%~1\BitField_byte.cs" goto copy_byte
fc /B "%~1\BitField_byte.cs.temp" "%~1\BitField_byte.cs" > nul
if ERRORLEVEL 1 goto copy_byte
goto ignore_byte
:copy_byte
copy "%~1\BitField_byte.cs.temp" "%~1\BitField_byte.cs" > nul
:ignore_byte
del "%~1\BitField_byte.cs.temp"
