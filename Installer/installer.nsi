;vATIS Installer
Unicode True

!include MUI2.nsh
!include x64.nsh
!include LogicLib.nsh
!include StrFunc.nsh

!include "Version.txt"

Name "vATIS"
BrandingText "vATIS"
OutFile ".\vATIS-Setup-${Version}.exe"
InstallDir "$LOCALAPPDATA\vATIS-4.0"
RequestExecutionLevel user

!define MUI_ABORTWARNING

!insertmacro MUI_PAGE_COMPONENTS

!define MUI_PAGE_HEADER_TEXT "vATIS Setup"
!define MUI_PAGE_HEADER_SUBTEXT "Choose the folder in which to install vATIS."
!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "Choose Install Location"
!define MUI_DIRECTORYPAGE_TEXT_TOP "Setup will install vATIS in the following folder. To install in a different folder, click Browse and select another folder. Click Install to start the installation."	
!insertmacro MUI_PAGE_DIRECTORY

!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_FINISHPAGE_RUN "$INSTDIR\Application\vATIS.exe"
!define MUI_FINISHPAGE_RUN_CHECKED
!define MUI_FINISHPAGE_RUN_TEXT "Launch vATIS"
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

Function .onInit
	;set client install location
	Push $INSTDIR
	ReadRegStr $INSTDIR HKCU "Software\vATIS-4.0" "vATIS"
	StrCmp $INSTDIR "" 0 +2
	Pop $INSTDIR
FunctionEnd

;Install
Section "vATIS" SecCopyUI
SectionIn RO
SetOutPath "$INSTDIR\Application"
File /r "..\Vatsim.Vatis\publish\*"
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
WriteUninstaller "$INSTDIR\Uninstall.exe"
WriteRegStr HKCU "Software\vATIS-4.0" "vATIS" $INSTDIR
SectionEnd

;Start Menu Shortcuts
Section "Start Menu Shortcuts" MenuShortcuts
createDirectory "$SMPROGRAMS\vATIS-4.0"
createShortCut "$SMPROGRAMS\vATIS-4.0\vATIS.lnk" "$INSTDIR\Application\vATIS.exe"
createShortCut "$SMPROGRAMS\vATIS-4.0\Uninstall vATIS.lnk" "$INSTDIR\Uninstall.exe"
SectionEnd

;Desktop Shortcut
Section "Desktop Shortcut" DesktopShortcut
CreateShortcut "$desktop\vATIS.lnk" "$INSTDIR\Application\vATIS.exe"
SectionEnd

;Uninstaller
Section "Uninstall"
Delete "$SMPROGRAMS\vATIS-4.0\vATIS.lnk"
Delete "$SMPROGRAMS\vATIS-4.0\Profile Editor.lnk"
Delete "$SMPROGRAMS\vATIS-4.0\Uninstall vATIS.lnk"
Delete "$DESKTOP\vATIS.lnk"
RMDir "$SMPROGRAMS\vATIS-4.0"
RMDir /r "$INSTDIR"
SectionEnd