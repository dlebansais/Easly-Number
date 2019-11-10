powershell -Command "(gc BitField_Template.cstext) -replace 'Template', 'uint' | Out-File -encoding UTF8 BitField_uint.cs"
powershell -Command "(gc BitField_Template.cstext) -replace 'Template', 'byte' | Out-File -encoding UTF8 BitField_byte.cs"
