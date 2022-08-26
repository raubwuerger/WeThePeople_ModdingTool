; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{5B3BDF55-B55B-418D-9F1D-7C35DF533E2D}
AppName=WeThePeople_ModdingTool
AppVersion=1.0.0
;AppVerName=WeThePeople_ModdingTool 1.0.0
DefaultDirName={autopf}\WeThePeople_ModdingTool
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
Compression=lzma
SolidCompression=yes
WizardStyle=modern
OutputBaseFilename=WeThePeople_ModdingTool
SetupIconFile=WtP_desktop_icon.ico
UninstallDisplayIcon=WtP_desktop_icon.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\WeThePeople_ModdingTool.exe"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\ICSharpCode.AvalonEdit.dll"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\Serilog.dll"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\Serilog.Sinks.Console.dll"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\Serilog.Sinks.File.dll"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\WeThePeople_ModdingTool.deps.json"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\WeThePeople_ModdingTool.dll"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\bin\Release\netcoreapp3.1\WeThePeople_ModdingTool.runtimeconfig.json"; DestDir: "{app}\program"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WtP_desktop_icon.ico"; DestDir: "{app}\program"; Flags: ignoreversion

Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\CIV4UnitInfos_OnlyClasses.xml"; DestDir: "{app}\templates\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Civ4YieldInfos_OnlyTypes.xml"; DestDir: "{app}\templates\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Harbours.xml"; DestDir: "{app}\templates\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Events\CIV4EventInfos_Done_Template.xml"; DestDir: "{app}\templates\Assets\XML\Events\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Events\CIV4EventInfos_Start_Template.xml"; DestDir: "{app}\templates\Assets\XML\Events\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Events\CIV4EventTriggerInfos_Done_Template.xml"; DestDir: "{app}\templates\Assets\XML\Events\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Events\CIV4EventTriggerInfos_Start_Template.xml"; DestDir: "{app}\templates\Assets\XML\Events\"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml"; DestDir: "{app}\templates\Assets\XML\Text"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\Python\EntryPoints\CvRandomEventInterface_Done_Template.py"; DestDir: "{app}\templates\Assets\Python\EntryPoints"; Flags: ignoreversion
Source: "D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\Python\EntryPoints\CvRandomEventInterface_Start_Template.py"; DestDir: "{app}\templates\Assets\Python\EntryPoints"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\.exe\OpenWithProgids"; ValueType: string; ValueName: "WeThePeople_ModdingToolFile.exe"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\WeThePeople_ModdingToolFile.exe"; ValueType: string; ValueName: ""; ValueData: "WeThePeople_ModdingTool File"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\WeThePeople_ModdingToolFile.exe\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\WeThePeople_ModdingTool.exe,0"
Root: HKA; Subkey: "Software\Classes\WeThePeople_ModdingToolFile.exe\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\WeThePeople_ModdingTool.exe"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\WeThePeople_ModdingTool.exe\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\WeThePeople_ModdingTool"; Filename: "{app}\program\WeThePeople_ModdingTool.exe"; IconFilename: "{app}\program\WtP_desktop_icon.ico"
Name: "{autodesktop}\WeThePeople_ModdingTool"; Filename: "{app}\program\WeThePeople_ModdingTool.exe"; Tasks: desktopicon; IconFilename: "{app}\program\WtP_desktop_icon.ico"

[Run]
Filename: "{app}\program\WeThePeople_ModdingTool.exe"; Description: "{cm:LaunchProgram,WeThePeople_ModdingTool}"; Flags: nowait postinstall skipifsilent

