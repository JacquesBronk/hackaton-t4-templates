using System.Text.Json;

using Maker.Docker;
using Maker.FileHelper;

Console.WriteLine("Let's make a base");

DockerFileMakerThingy dockerFileMakerThingy = new DockerFileMakerThingy();

dockerFileMakerThingy.MakeComposeDockerfile(new [] {"test-service", "front-end" }, CommonPaths.AppPath);
dockerFileMakerThingy.MakeDotnetDockerfile("test-service", Path.Combine(CommonPaths.AppPath, "backend"));
dockerFileMakerThingy.MakeNginxDockerfile("test-ui", Path.Combine(CommonPaths.AppPath, "front-end"));

string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Definition.json");

Console.WriteLine("Base path: " + basePath);

var jsonFilePath = basePath;
string jsonContent = File.ReadAllText(jsonFilePath);
var data = JsonSerializer.Deserialize<JsonDocument>(jsonContent);
