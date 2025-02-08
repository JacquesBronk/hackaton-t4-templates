using System.Text;

namespace Maker.Docker;

public class DockerFileMakerThingy
{
    //Dotnet Dockerfile Maker
    public void MakeDotnetDockerfile(string projectName, string dockerfilePath)
    {
        if (!Directory.Exists(dockerfilePath))
        {
            Directory.CreateDirectory(dockerfilePath);
        }
        
        if (!File.Exists(Path.Combine(dockerfilePath, "Dockerfile")))
        {
            //Close the file after creating it
            using (File.Create(Path.Combine(dockerfilePath, "Dockerfile")))
            {
            }
            
        }
        
        string apiDockerFile = $@"
        # Stage 1: Build and install tools
        FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
        WORKDIR /app
        
        # Install dotnet tools
        RUN dotnet tool install --global dotnet-trace \
            && dotnet tool install --global dotnet-dump \
            && dotnet tool install --global dotnet-counters \
            && dotnet tool install --global dotnet-gcdump \
            && dotnet tool install --global dotnet-coverage
        
        ENV PATH=""$PATH:/root/.dotnet/tools/tools""
        
        # Copy the tools to a temporary location
        WORKDIR /tools
        RUN cp -r /root/.dotnet/tools .
        
        # Stage 2: Create the runtime image
        FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
        WORKDIR /app
        
        # Install required dependencies and debugging tools
        RUN apt-get update \
            && apt-get install -y --no-install-recommends \
                libc6-dev \
                libgdiplus \
                libx11-dev \
                procps \
                strace \
                lsof \
                gdb \
            && rm -rf /var/lib/apt/lists/*
        
        # Copy the tools from the build stage
        COPY --from=build /tools /root/.dotnet/tools
        
        # Copy the application files
        COPY ./publish .
        
        # Ensure the tools are in the PATH
        ENV PATH=""$PATH:/root/.dotnet/tools/tools""
        ENV ASPNETCORE_URLS=http://*:80
        
        ENTRYPOINT [""dotnet"", ""{projectName}.dll""]
        ";
        
        File.WriteAllText(Path.Combine(dockerfilePath, "Dockerfile"), apiDockerFile);
    }
    
    //Nginx Dockerfile Maker
    public void MakeNginxDockerfile(string nginxPublishPath, string nginxDockerPath)
    {

        if (!Directory.Exists(nginxDockerPath))
        {
            Directory.CreateDirectory(nginxDockerPath);
        }
        if (!File.Exists(Path.Combine(nginxDockerPath, "Dockerfile")))
        {
            using (File.Create(Path.Combine(nginxDockerPath, "Dockerfile"), 1024, FileOptions.Asynchronous))
            {
            }
        }
        
        string nginxFile = $@"
        FROM nginx:latest
        COPY {nginxPublishPath} /etc/nginx/nginx.conf
        COPY {nginxPublishPath} /etc/nginx/conf.d/default.conf
        ";
        
        File.WriteAllText(Path.Combine(nginxDockerPath, "Dockerfile"), nginxFile);
    }
    
    
    //Compose Dockerfile Maker
    public void MakeComposeDockerfile(string[] services, string dockerfilePath, int publicPortStart = 8000, int privatePortStart = 80)
    {

        StringBuilder composeFile = new StringBuilder();
        composeFile.AppendLine("services:");
        composeFile.AppendLine("    sqlserver:");
        composeFile.AppendLine("        image: mcr.microsoft.com/mssql/server:2019-latest");
        composeFile.AppendLine("        environment:");
        composeFile.AppendLine("            SA_PASSWORD: \"Password123\"");
        composeFile.AppendLine("            ACCEPT_EULA: \"Y\"");
        composeFile.AppendLine("        ports:");
        composeFile.AppendLine("            - \"1433:1433\"");
        composeFile.AppendLine();
        foreach (var serviceName in services)
        {
            publicPortStart++;
            composeFile.AppendLine(MakeService(serviceName, Path.Combine(dockerfilePath, serviceName), publicPortStart, privatePortStart));
        }
        File.WriteAllText(Path.Combine(dockerfilePath, "docker-compose.yml"), composeFile.ToString());
    }    
    
    private string MakeService(string serviceName, string servicePath, int publicPort, int privatePort)
    {
        return $@"
    {serviceName}:
       build:
         context: {servicePath}
         dockerfile: Dockerfile
       ports:
        - {publicPort}:{privatePort}
            ";
    }
    
}