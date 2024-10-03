This is a Tool for Creating UE StandaloneApplication.

It Only work for UE Built With Source Version.

### Usage

```shell
usage: main.py [-h] [-uproject UPROJECT] [-output OUTPUT] name

Create Standalone Program In UE5

positional arguments:
  name                Program Name

options:
  -h, --help          show this help message and exit
  -uproject UPROJECT  .uproject file path, Program will output to uproject_path/Source/Programs
  -output OUTPUT      specific output path
```

把生成的目录，放到 ue 项目 (uproject) 下的 Source/Programs/，然后重新生成项目文件 (project files)

put the output folder under some ue project Source/Programs/， and regenerate project files.

### Q & A

如果遇到编译错误 (when run into compile error): `10>VersionResource.inl(5): Error RC1015 : cannot open include file 'Launch/Resources/Windows/resource.h'.`

* 继续编译一次即可，just build project again
