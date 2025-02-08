using System.Text.Json;
using Maker.ShellCommands;
using Maker.FileHelper;

if(!Directory.Exists(CommonPaths.FrontEnd))
{
    Directory.CreateDirectory(CommonPaths.FrontEnd);
}

if(!Directory.Exists(CommonPaths.BackEnd))
{
    Directory.CreateDirectory(CommonPaths.BackEnd);
}


// await DotnetExecutor.BuildExecutor()
//     .ForProject("yourproject.csproj")
//     .WithWorkingDirectory("\\if\\it\\in\\another\\directory")
//     .WithVerbosityLevel(VerbosityLevel.Normal)
//     .DotnetBuild();


