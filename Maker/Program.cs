// See https://aka.ms/new-console-template for more information

using Maker.Docker;
using Maker.FileHelper;

Console.WriteLine("Let's make a base");

DockerFileMakerThingy dockerFileMakerThingy = new DockerFileMakerThingy();

dockerFileMakerThingy.MakeComposeDockerfile(new [] {"test-service", "front-end" }, CommonPaths.AppPath);
dockerFileMakerThingy.MakeDotnetDockerfile("test-service", Path.Combine(CommonPaths.AppPath, "backend"));
dockerFileMakerThingy.MakeNginxDockerfile("test-ui", Path.Combine(CommonPaths.AppPath, "front-end"));