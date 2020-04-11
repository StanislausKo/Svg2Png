; The name of the installer
Name "Svg2Png"

; The file to write
OutFile "Svg2Png.exe"

; The default installation directory
InstallDir "$DESKTOP\Svg2Png"

; Request application privileges for Windows Vista
RequestExecutionLevel user

; Pages
Page Directory
Page Instfiles
;--------------------------------
; The stuff to install
Section "Files" 
  ; Set output path to the installation directory.
  ; CreateDirectory "$INSTDIR"
  SetOutPath "$INSTDIR"
  
  ; Put files there
  File /r "Bin\Release\*.dll"
  File "Bin\Release\*.exe"
  
SectionEnd ; end the section
