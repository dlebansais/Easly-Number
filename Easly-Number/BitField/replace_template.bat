rem echo Replacing in folder '%~1'

powershell -Command "(gc %~1\BitField_Template.cstext) -replace 'Template', 'uint' | Out-File -encoding UTF8 %~1\BitField_uint.cs.temp"
fc /B "%~1\BitField_uint.cs.temp" "%~1\BitField_uint.cs" > nul
if ERRORLEVEL 1 copy "%~1\BitField_uint.cs.temp" "%~1\BitField_uint.cs" > nul
del "%~1\BitField_uint.cs.temp"

powershell -Command "(gc %~1\BitField_Template.cstext) -replace 'Template', 'byte' | Out-File -encoding UTF8 %~1\BitField_byte.cs.temp"
fc /B "%~1\BitField_byte.cs.temp" "%~1\BitField_byte.cs" > nul
if ERRORLEVEL 1 copy "%~1\BitField_byte.cs.temp" "%~1\BitField_byte.cs" > nul
del "%~1\BitField_byte.cs.temp"
