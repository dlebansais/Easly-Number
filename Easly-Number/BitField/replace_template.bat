rem echo Replacing in folder '%~1'
powershell -Command "(gc %~1/BitField_Template.cstext) -replace 'Template', 'uint' | Out-File -encoding UTF8 %~1/BitField_uint.cs"
powershell -Command "(gc %~1/BitField_Template.cstext) -replace 'Template', 'byte' | Out-File -encoding UTF8 %~1/BitField_byte.cs"
