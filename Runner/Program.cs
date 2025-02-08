// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Runner.ShellCommands;

{
    //find git root path
    Console.WriteLine("We tried doing tings");
    var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

    while (true)
    {
        string gitDir = Path.Combine(di.FullName, ".git");

        if (Directory.Exists(gitDir))
            break; // Found the root

        di = di.Parent;

        if (di == null)
            break; // Traverse to root
    }

    Console.WriteLine(di?.FullName);
    
    await DotnetExecutor.BuildExecutor()
        .InstallMonoT4();
     

               
    // find all tt file extenstion paths
               
    var ttFiles = Directory.GetFiles(Path.Combine(di?.FullName, "Maker"), "*.tt", SearchOption.AllDirectories);

    foreach (var ttFile in ttFiles)
    {
        await DotnetExecutor.BuildExecutor()
            .DotnetGenerateT4File(ttFile);
    }
}