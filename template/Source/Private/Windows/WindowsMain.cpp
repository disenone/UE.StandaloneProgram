#include "RealExecutionMain.h"
#include <windows.h>

int WinMain(
	_In_ HINSTANCE InhInstance,
	_In_opt_ HINSTANCE InhPrevInstance,
	_In_ LPSTR InlpCmdLine,
	_In_ int InShowCmd
)
{
	return RealExecutionMain(GetCommandLineW());
}