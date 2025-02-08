using System.Text.Json;

using Maker.FileHelper;

using Maker.ShellCommands;

Console.WriteLine("Let's make a base");

string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Definition.json");

Console.WriteLine("Base path: " + basePath);

var jsonFilePath = basePath;
string jsonContent = File.ReadAllText(jsonFilePath);
var data = JsonSerializer.Deserialize<JsonDocument>(jsonContent);

// await DotnetExecutor.BuildExecutor()
//     .ForProject("yourproject.csproj")
//     .WithWorkingDirectory("\\if\\it\\in\\another\\directory")
//     .WithVerbosityLevel(VerbosityLevel.Normal)
//     .DotnetBuild();


