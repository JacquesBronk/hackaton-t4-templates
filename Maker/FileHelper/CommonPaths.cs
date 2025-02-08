namespace Maker.FileHelper;

public static class CommonPaths
{
    public static string AppPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

    public static string FrontEnd => Path.Combine(AppPath, "FrontEnd");
    
    public static string BackEnd => Path.Combine(AppPath, "BackEnd");


}