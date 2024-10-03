#include <iostream>
#include <vector>
#include <string>
#include "BaseProgramFile.h"
#include "ProgramTemplateText/AllProgramTemplateFiles.h"

using namespace std;

int main(int argc,char* argv[])
{
    std::cout << "file list size: " << BaseProgramFile::file_list.size() << ", " << (void*)&BaseProgramFile::file_list << std::endl;
	if(argc<2){
		std::cout<<"Usage:\n\t"<<"create_program.exe ProgramName"<<std::endl;
		return -1;
	}

	std::string ProgramName=std::string(argv[1]);

    std::cout << "Creating Standalone Program: " << argv[1] << std::endl;
    std::cout << "file list size: " << BaseProgramFile::file_list.size() << ", " << (void*)&BaseProgramFile::file_list << std::endl;

	for(auto& index: BaseProgramFile::file_list)
	{
		index->Init(ProgramName);
        // std::cout << "1111" << std::endl;
		index->WriteToFile();
        // std::cout << "2222" << std::endl;
	}

	FILE* loadFP=fopen((ProgramName+"/Resources/Icon.ico").c_str(),"wb");
	fwrite(BinData,sizeof(BinData),1,loadFP);
	fclose(loadFP);
	std::cout<<"Create Standalone Program Successed!"<<std::endl;
	return 0;
}