OpenCover.Console.exe -oldstyle -register:user -target:"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe" -targetargs:"C:\Projects\MicroservicingAround\MsingBackend\FileReaderTest\bin\Debug\netcoreapp2.1\FileReaderTest.dll" -filter:"+[*]* -[*Test]*" -output:opencovertests.xml -mergebyhash
"C:\Projects\ReportGenerator\net47\ReportGenerator.exe" -reports:opencovertests.xml -targetdir:coverage
coverage\index.htm
