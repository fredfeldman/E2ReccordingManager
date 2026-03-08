; Enigma2 Recording Manager - Inno Setup Script
; Requires Inno Setup 6.x - https://jrsoftware.org/isinfo.php
;
; Before compiling:
;   1. Publish the project in Release mode (self-contained or framework-dependent).
;      Recommended publish command (self-contained, single output folder):
;        dotnet publish ..\E2ReccordingManager.csproj -c Release -r win-x64 --self-contained true -o ..\..\publish
;   2. Adjust SourceDir below if you use a different output folder.
;      For a plain Release build (framework-dependent) use:
;        SourceDir=..\..\E2ReccordingManager\bin\Release\net8.0-windows\

#define AppName       "Enigma2 Recording Manager"
#define AppVersion    "1.0.0"
#define AppPublisher  "Fred Feldman"
#define AppURL        "https://github.com/fredfeldman/E2ReccordingManager"
#define AppExeName    "E2ReccordingManager.exe"
#define AppId         "{{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}"

; Path to the published/built output – adjust if needed
#define SourceDir     "..\..\publish"

[Setup]
AppId={#AppId}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
AllowNoIcons=yes
; Installer output location (relative to this .iss file)
OutputDir=Output
OutputBaseFilename=E2RecordingManager-{#AppVersion}-Setup
SetupIconFile=
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
MinVersion=10.0
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
UninstallDisplayIcon={app}\{#AppExeName}
UninstallDisplayName={#AppName}
VersionInfoVersion={#AppVersion}
VersionInfoCompany={#AppPublisher}
VersionInfoDescription={#AppName} Installer
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#AppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon";    Description: "{cm:CreateDesktopIcon}";    GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Include all files from the publish/build output folder
Source: "{#SourceDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#AppName}";            Filename: "{app}\{#AppExeName}"
Name: "{group}\{cm:UninstallProgram,{#AppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}";      Filename: "{app}\{#AppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#AppName}"; Filename: "{app}\{#AppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Remove application settings/data left behind in AppData
Type: filesandordirs; Name: "{localappdata}\{#AppName}"
Type: filesandordirs; Name: "{userappdata}\{#AppName}"

; [Code] section removed – not needed for self-contained builds.
; The .NET 8 runtime is bundled inside the published output folder.
