;vATIS Installer
Unicode True

!include MUI2.nsh
!include x64.nsh
!include LogicLib.nsh
!include StrFunc.nsh

!include "Version.txt"

Name "vATIS"
BrandingText "vATIS ${Version}"
OutFile ".\vATIS-Setup-${Version}.exe"
InstallDir "$LOCALAPPDATA\vATIS-4.0"
RequestExecutionLevel user

!define MUI_ABORTWARNING

!define MUI_WELCOMEPAGE_TEXT "This installer will guide you through the installation of vATIS."
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS

!define MUI_PAGE_HEADER_TEXT "vATIS Installation"
!define MUI_PAGE_HEADER_SUBTEXT "Choose folder to install vATIS"
!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "vATIS Install Location"
!define MUI_DIRECTORYPAGE_TEXT_TOP "The setup will install vATIS in the following folder. It is recommended you leave it set to the local application data folder to prevent permission issues. To install in a different folder, click Browse and select another folder."	
!insertmacro MUI_PAGE_DIRECTORY

!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_FINISHPAGE_RUN "$INSTDIR\vATIS.exe"
!define MUI_FINISHPAGE_RUN_CHECKED
!define MUI_FINISHPAGE_RUN_TEXT "Start vATIS"
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

Function .onInit
	;set client install location
	Push $INSTDIR
	ReadRegStr $INSTDIR HKLM "Software\vATIS-4.0" "vATIS"
	StrCmp $INSTDIR "" 0 +2
	Pop $INSTDIR
FunctionEnd

;Install
Section "vATIS" SecCopyUI
SectionIn RO
SetOutPath "$INSTDIR"
File /r ".\Vatis.Client\publish\*"
WriteUninstaller "$INSTDIR\Uninstall.exe"
WriteRegStr HKLM "Software\vATIS-4.0" "vATIS" $INSTDIR
SectionEnd

;Start Menu Shortcuts
Section "Start Menu Shortcuts" MenuShortcuts
createDirectory "$SMPROGRAMS\vATIS-4.0"
createShortCut "$SMPROGRAMS\vATIS-4.0\vATIS.lnk" "$INSTDIR\vATIS.exe"
createShortCut "$SMPROGRAMS\vATIS-4.0\Profile Editor.lnk" "$INSTDIR\vATIS.exe" "/editor"
createShortCut "$SMPROGRAMS\vATIS-4.0\Uninstall vATIS.lnk" "$INSTDIR\Uninstall.exe"
SectionEnd

;Desktop Shortcut
Section "Desktop Shortcut" DesktopShortcut
CreateShortcut "$desktop\vATIS.lnk" "$INSTDIR\vATIS.exe"
SectionEnd

;Delete AppConfig.json
Section /o "Delete Config File" EmptyConfig
Delete $INSTDIR\AppConfig.json
Delete $INSTDIR\ProfileEditorConfig.json
SectionEnd

;Uninstaller
Section "Uninstall"
Delete "$SMPROGRAMS\vATIS-4.0\vATIS.lnk"
Delete "$SMPROGRAMS\vATIS-4.0\ProfileEditor.lnk"
Delete "$SMPROGRAMS\vATIS-4.0\Uninstall vATIS.lnk"
Delete "$DESKTOP\vATIS.lnk"
RMDir "$SMPROGRAMS\vATIS-4.0"
RMDir /r "$INSTDIR"
SectionEnd