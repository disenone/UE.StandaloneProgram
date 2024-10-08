using UnrealBuildTool;
using EpicGames.Core;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

public class {_ProgramName_} : ModuleRules
{

    private const string ProgramName = "{_ProgramName_}";

    private const string PackageWin = @"
@echo off

setlocal

set PROJECT_NAME={_ProgramName_}
set ENGINE_PATH={_EnginePath_}
set CONFIGURATION={_Configuration_}
set OUTPUT_FILE={_OutputFile_}.exe
set TAB=	

set PROJECT_PATH=%~dp0
call :NormalizePath %PROJECT_PATH%..\\..\\..
set UPROJECT_PATH=%RETVAL%

set OUTPUT_PATH=%PROJECT_PATH%Package\\%CONFIGURATION%\\%PROJECT_NAME%

powershell -Command ""Write-Host 'Packaging [%PROJECT_NAME%]' -ForegroundColor Red""

if not exist ""%OUTPUT_PATH%\\Engine\\Binaries\\Win64"" mkdir ""%OUTPUT_PATH%\\Engine\\Binaries\\Win64""
if not exist ""%OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt53l"" mkdir ""%OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt53l""
if not exist ""%OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt64l"" mkdir ""%OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt64l""
if not exist ""%OUTPUT_PATH%\\Engine\\Content\\Slate"" mkdir ""%OUTPUT_PATH%\\Engine\\Content\\Slate""
if not exist ""%OUTPUT_PATH%\\Engine\\Shaders\\StandaloneRenderer"" mkdir ""%OUTPUT_PATH%\\Engine\\Shaders\\StandaloneRenderer""

echo %TAB%copy %UPROJECT_PATH%\\Binaries\\Win64\\%OUTPUT_FILE% -^> %OUTPUT_PATH%\\Engine\\Binaries\\Win64
copy %UPROJECT_PATH%\\Binaries\\Win64\\%OUTPUT_FILE% %OUTPUT_PATH%\\Engine\\Binaries\\Win64 | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %UPROJECT_PATH%\\Binaries\\Win64\\tbbmalloc.dll -^> %OUTPUT_PATH%\\Engine\\Binaries\\Win64
copy %UPROJECT_PATH%\\Binaries\\Win64\\tbbmalloc.dll %OUTPUT_PATH%\\Engine\\Binaries\\Win64 | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %UPROJECT_PATH%\\Binaries\\Win64\\tbb.dll -^> %OUTPUT_PATH%\\Engine\\Binaries\\Win64
copy %UPROJECT_PATH%\\Binaries\\Win64\\tbb.dll %OUTPUT_PATH%\\Engine\\Binaries\\Win64 | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %ENGINE_PATH%\\Content\\Internationalization\\English\\icudt53l -^> %OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt53l
xcopy /y/i/s/e/q %ENGINE_PATH%\\Content\\Internationalization\\English\\icudt53l %OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt53l | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %ENGINE_PATH%\\Content\\Internationalization\\English\\icudt64l -^> %OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt64l
xcopy /y/i/s/e/q %ENGINE_PATH%\\Content\\Internationalization\\English\\icudt64l %OUTPUT_PATH%\\Engine\\Content\\Internationalization\\icudt64l | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %ENGINE_PATH%\\Content\\Slate -^> %OUTPUT_PATH%\\Engine\\Content\\Slate
xcopy /y/i/s/e/q %ENGINE_PATH%\\Content\\Slate %OUTPUT_PATH%\\Engine\\Content\\Slate | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %ENGINE_PATH%\\Shaders\\StandaloneRenderer -^> %OUTPUT_PATH%\\Engine\\Shaders\\StandaloneRenderer
xcopy /y/i/s/e/q %ENGINE_PATH%\\Shaders\\StandaloneRenderer %OUTPUT_PATH%\\Engine\\Shaders\\StandaloneRenderer | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo %TAB%copy %PROJECT_PATH%Resources\\icon.ico -^> %OUTPUT_PATH%\\Engine\\Content\\Slate\\Icons
copy %PROJECT_PATH%Resources\\icon.ico %OUTPUT_PATH%\\Engine\\Content\\Slate\\Icons | set /p result=
if %ERRORLEVEL% neq 0 (
    echo %TAB%%result%
    echo %TAB%failed: %ERRORLEVEL%
    goto :eof
)

echo START /B Engine\\Binaries\\Win64\\%OUTPUT_FILE% > %OUTPUT_PATH%\\%PROJECT_NAME%.bat

powershell -Command ""Write-Host 'Done Packaging [%PROJECT_NAME%], call [%OUTPUT_PATH%\\%PROJECT_NAME%.bat] to run' -ForegroundColor Red""

:eof
endlocal
pause
exit /b %ERRORLEVEL%

:NormalizePath
  SET RETVAL=%~f1
  EXIT /B 0

";

	public {_ProgramName_}(ReadOnlyTargetRules Target) : base(Target)
	{
        PrivatePCHHeaderFile = "Source/Public/RealExecutionMain.h";

        PublicIncludePaths.AddRange(
            new string[]
            {
                Path.Combine(EngineDirectory, "Source/Runtime/Launch/Public"),
                "Programs/{_ProgramName_}/Source/Public",
            });
        PrivateIncludePaths.AddRange(
            new string[]
            {
                Path.Combine(EngineDirectory, "Source/Runtime/Launch/Private"),  // For LaunchEngineLoop.cpp include
                Path.Combine(EngineDirectory, "Source/Runtime"),
                "Programs/{_ProgramName_}/Source/Private"
            });

        PrivateDependencyModuleNames.AddRange(
            new string[] {
                "AppFramework",
                "Core",
                "ApplicationCore",
                "Projects",
                "Slate",
                "SlateCore",
                "InputCore",
                "SlateReflector",
                "StandaloneRenderer"
            }
        );

        GeneratePackageScript();
    }

    private void GeneratePackageScript()
    {
        Dictionary<string, string> PackageArgs = new Dictionary<string, string>();
        PackageArgs["{_EnginePath_}"] = EngineDirectory;
        PackageArgs["{_Configuration_}"] = Target.Configuration.ToString();

        if (Target.Configuration == UnrealTargetConfiguration.Development)
            PackageArgs["{_OutputFile_}"] = ProgramName;

        else
            PackageArgs["{_OutputFile_}"] = string.Format("{0}-Win64-{1}", ProgramName, Target.Configuration.ToString());

        string PackageText = PackageWin;

        foreach (var item in PackageArgs)
        {
            PackageText = PackageText.Replace(item.Key, item.Value);
        }

        var SaveFile = Path.Join(ModuleDirectory, "Package.bat");
        File.WriteAllText(SaveFile, PackageText);

        Console.WriteLine("Package.bat for [{0}] is saved to [{1}]", ProgramName, SaveFile);
    }
}
