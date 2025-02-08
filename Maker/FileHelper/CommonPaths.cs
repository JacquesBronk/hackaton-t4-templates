namespace Maker.FileHelper;

public static class CommonPaths
{
    public static string AppPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
    public static string AppDataPath => Path.Combine(AppPath, "AppData");
    
    public static string TempPath => Path.Combine(AppDataPath, "Temp");
    
    public static string GeneratorPath => Path.Combine(AppPath, "Generated");

    public static string DockerFilePath => AppPath;
}