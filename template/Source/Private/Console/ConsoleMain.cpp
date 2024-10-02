#include "RealExecutionMain.h"

namespace StringLibs {
	FString Conv_CharArrayToFString(int elementNum, char* CharArray[]);
}

int main(int argc,char *argv[])
{
    return RealExecutionMain(*StringLibs::Conv_CharArrayToFString(argc, argv));
}


namespace StringLibs {
	FString Conv_CharArrayToFString(int elementNum, char* CharArray[])
	{
		FString local_SavedCommandLine = TEXT("");

		for (int32 Option = 0; Option < elementNum; Option++)
		{
			local_SavedCommandLine += TEXT(" ");
			FString Argument(ANSI_TO_TCHAR(CharArray[Option]));
			if (Argument.Contains(TEXT(" ")))
			{
				if (Argument.Contains(TEXT("=")))
				{
					FString ArgName;
					FString ArgValue;
					Argument.Split(TEXT("="), &ArgName, &ArgValue);
					Argument = FString::Printf(TEXT("%s=\"%s\""), *ArgName, *ArgValue);
				}
				else
				{
					Argument = FString::Printf(TEXT("\"%s\""), *Argument);
				}
			}
			local_SavedCommandLine += Argument;
		}
		return local_SavedCommandLine;
	}
}