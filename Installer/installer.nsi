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
InstallDir "$LOCALAPPDATA\org.vatsim.vatis"
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
	ReadRegStr $INSTDIR HKCU "Software\vATIS" "vATIS"
	StrCmp $INSTDIR "" 0 +2
	Pop $INSTDIR
FunctionEnd

;Install
Section "vATIS" SecCopyUI
SectionIn RO
SetOutPath "$INSTDIR\Application"
File /r "..\Vatsim.Vatis\publish\*"
WriteUninstaller "$INSTDIR\Uninstall.exe"
WriteRegStr HKCU "Software\vATIS" "vATIS" $INSTDIR
SectionEnd

;Start Menu Shortcuts
Section "Start Menu Shortcuts" MenuShortcuts
createDirectory "$SMPROGRAMS\vATIS"
createShortCut "$SMPROGRAMS\vATIS\vATIS.lnk" "$INSTDIR\Application\vATIS.exe"
createShortCut "$SMPROGRAMS\vATIS\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
SectionEnd

;Desktop Shortcut
Section "Desktop Shortcut" DesktopShortcut
CreateShortcut "$desktop\vATIS.lnk" "$INSTDIR\vATIS.exe"
SectionEnd

;Uninstaller
Section "Uninstall"
Delete "$SMPROGRAMS\vATIS\vATIS.lnk"
Delete "$SMPROGRAMS\vATIS\Uninstall.lnk"
Delete "$DESKTOP\vATIS.lnk"
RMDir "$SMPROGRAMS\vATIS"
RMDir /r "$INSTDIR"
SectionEnd