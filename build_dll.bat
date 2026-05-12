@echo off
echo Building LogParserDLL...
cd LogParserDLL
msbuild LogParserDLL.csproj /p:Configuration=Debug /p:Platform=AnyCPU
cd ..
echo Done!
pause
