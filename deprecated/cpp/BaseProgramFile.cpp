#include "BaseProgramFile.h"
#include <iostream>

std::vector<class BaseProgramFile*> BaseProgramFile::file_list;

BaseProgramFile::BaseProgramFile()
{
	BaseProgramFile::file_list.push_back(this);
    std::cout << "BaseProgramFile(): " << BaseProgramFile::file_list.size() << ", " << (void*)&BaseProgramFile::file_list <<  std::endl;

}


void BaseProgramFile::Init(const std::string& pProgramName)
{
	ProgramName=pProgramName;
}


void BaseProgramFile::WriteToFile()
{
	StringTools::create_recursion_dir("",file_path);
	std::string local_file_path=file_path+"\\"+file_name;
	std::cout<<"write file_path is "<<local_file_path<<std::endl;
	std::fstream out_file(local_file_path,std::ofstream::out);
	out_file<<StringTools::replace_substring(file_content,"ProgramTemplate",ProgramName)<<std::endl;
	out_file.close();
}
