#include "RealExecutionMain.h"

#include <Framework/Docking/TabManager.h>
#include <Framework/Application/SlateApplication.h>
#include <Modules/ModuleManager.h>
#include <Windows/AllowWindowsPlatformTypes.h>
#include <StandaloneRenderer.h>

#include <windows.h>
#include <iostream>

IMPLEMENT_APPLICATION({_ProgramName_}, "{_ProgramName_}");

#define LOCTEXT_NAMESPACE "{_ProgramName_}"

int RealExecutionMain(const TCHAR* pCmdLine)
{
    std::cout << "Hello UE4!" << std::endl;

	// init Engine
	GEngineLoop.PreInit(GetCommandLineW());
	FSlateApplication::InitializeAsStandaloneApplication(GetStandardStandaloneRenderer());

	// create a test window
	FGlobalTabmanager::Get()->SetApplicationTitle(LOCTEXT("AppTitle", "{_ProgramName_}"));
	TSharedPtr<SWindow> MainWindow = SNew(SWindow)
		.Title(LOCTEXT("MainWindow_Title", "Hello UE4!"))
		.ScreenPosition(FVector2D(800, 600))
		.ClientSize(FVector2D(900, 600))
		.AutoCenter(EAutoCenter::None);

	FSlateApplication::Get().AddWindow(MainWindow.ToSharedRef());

	while (!IsEngineExitRequested())
	{
		FSlateApplication::Get().Tick();
		FSlateApplication::Get().PumpMessages();
	}
	FSlateApplication::Shutdown();

	return 0;
}

#undef LOCTEXT_NAMESPACE