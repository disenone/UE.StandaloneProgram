using UnrealBuildTool;
using EpicGames.Core;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

public class {_ProgramName_} : ModuleRules
{
	public {_ProgramName_}(ReadOnlyTargetRules Target) : base(Target)
	{
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
    }
}