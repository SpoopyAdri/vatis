!include MUI2.nsh
!include x64.nsh
!include LogicLib.nsh
!include StrFunc.nsh

!include "Version.txt"

Name "vATIS"
BrandingText "vATIS"
OutFile ".\vATIS-Update-${Version}.exe"
InstallDir "$LOCALAPPDATA\vATIS-4.0"
RequestExecutionLevel user
SilentInstall silent

Function .onInit
	;set client install location
	Push $INSTDIR
	ReadRegStr $INSTDIR HKCU "Software\vATIS-4.0" "vATIS"
	StrCmp $INSTDIR "" 0 +2
	Pop $INSTDIR
FunctionEnd

Section
  ;Delete old files
  Delete "$SMPROGRAMS\vATIS-4.0\Profile Editor.lnk"
  Delete $INSTDIR\airports.json
  Delete $INSTDIR\aspnetcorev2_inprocess.dll
  Delete $INSTDIR\D3DCompiler_47_cor3.dll
  Delete $INSTDIR\navaids.json
  Delete $INSTDIR\PenImc_cor3.dll
  Delete $INSTDIR\PresentationNative_cor3.dll
  Delete $INSTDIR\ProfileEditorConfig.json
  Delete $INSTDIR\sni.dll
  Delete $INSTDIR\vATIS.exe
  Delete $INSTDIR\VatsimAuth.dll
  Delete $INSTDIR\vcruntime140_cor3.dll
  Delete $INSTDIR\wpfgfx_cor3.dll

  ;Copy new files
  SetOutPath "$INSTDIR\Application"
  File /r "..\Vatsim.Vatis\update\*"

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  WriteRegStr HKCU "Software\vATIS-4.0" "vATIS" $INSTDIR

  ;Start vATIS
  ExecShell "" $INSTDIR\Application\vATIS.exe
SectionEnd

Section "Uninstall"
  Delete "$SMPROGRAMS\vATIS-4.0\vATIS.lnk"
  Delete "$SMPROGRAMS\vATIS-4.0\Profile Editor.lnk"
  Delete "$SMPROGRAMS\vATIS-4.0\Uninstall vATIS.lnk"
  Delete "$DESKTOP\vATIS.lnk"
  RMDir "$SMPROGRAMS\vATIS-4.0"
  RMDir /r "$INSTDIR"
SectionEnd