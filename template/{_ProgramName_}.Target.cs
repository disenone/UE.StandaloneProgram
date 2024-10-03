using UnrealBuildTool;
using System.Collections.Generic;
using System.IO;

[SupportedPlatforms(UnrealPlatformClass.All)]
public class {_ProgramName_}Target : TargetRules
{
    public {_ProgramName_}Target(TargetInfo Target) : base(Target)
    {
        Type = TargetType.Program;
        LinkType = TargetLinkType.Monolithic;
        DefaultBuildSettings = BuildSettingsVersion.V4;
        IncludeOrderVersion = EngineIncludeOrderVersion.Latest;

        Name = "{_ProgramName_}";
        LaunchModuleName = "{_ProgramName_}";

        // Whether to compile WITH_EDITORONLY_DATA disabled. Only Windows will use this, other platforms force this to false.
        //bBuildWithEditorOnlyData = false;

        // Compile out references from Core to the rest of the engine
        bCompileAgainstEngine = false;

        // Enabled for all builds that include the CoreUObject project. Disabled only when building standalone apps that only link with Core.
        bCompileAgainstCoreUObject = true;

		// Logs are still useful to print the results
		bUseLoggingInShipping = true;

        // Whether to include plugin support.
        bCompileWithPluginSupport = true;

        // Enable exceptions for all modules
        bForceEnableExceptions = false;

        // Enable RTTI for all modules.
        // bForceEnableRTTI = true;

        // If ture the program entrance is WinMain, otherwise entrance is main
        bIsBuildingConsoleApplication = false;

		// use unity builds by default
		bUseUnityBuild = true;

        // PostBuildSteps, auto package the program
		var ModuleDirectory = Path.GetDirectoryName(__FILE__());
		var PackageFile = Path.Join(ModuleDirectory, "Package.bat");
		PostBuildSteps.Add(string.Format("call {0}", PackageFile));
    }

    static string __FILE__([System.Runtime.CompilerServices.CallerFilePath] string fileName = "")
    {
	    return fileName;
    }
}