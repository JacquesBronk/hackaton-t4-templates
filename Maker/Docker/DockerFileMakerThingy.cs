namespace Maker.Docker;

public class DockerFileMakerThingy
{
    //Dotnet Dockerfile Maker
    public string MakeDotnetDockerfile(string projectName, string projectPath, string dockerfilePath)
    {
        return $@"
            FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
            WORKDIR /source

            COPY {projectPath}/*.csproj .
            RUN dotnet restore

            COPY {projectPath} .
            RUN dotnet publish -c release -o /app --no-restore

            FROM mcr.microsoft.com/dotnet/aspnet:5.0
            WORKDIR /app
            COPY --from=build /app .

            ENTRYPOINT [""dotnet"", ""{projectName}.dll""]
        ";
    }
    
    //Nginx Dockerfile Maker
    public string MakeNginxDockerfile(string nginxConfigPath, string nginxConfigFilePath)
    {
        return $@"
            FROM nginx:latest
            COPY {nginxConfigPath} /etc/nginx/nginx.conf
            COPY {nginxConfigFilePath} /etc/nginx/conf.d/default.conf
        ";
    }
    
    
    //Compose Dockerfile Maker
    
}