// See https://aka.ms/new-console-template for more information

using Maker.ShellCommands;

Console.WriteLine("Let's make a base");

await DotnetExecutor.BuildExecutor()
    .ForProject("yourproject.csproj")
    .WithWorkingDirectory("\\if\\it\\in\\another\\directory")
    .WithVerbosityLevel(VerbosityLevel.Normal)
    .DotnetBuild();

