namespace Maker.ShellCommands;

public class DotnetExecutor : ShellExecutor
{
    private string _project;
    private bool _useProjectPath;
    private VerbosityLevel _verbosityLevel = VerbosityLevel.Quiet;
    private string _output;
    
    public new static DotnetExecutor BuildExecutor() => new DotnetExecutor();
    
    public DotnetExecutor ForProject(string project)
    {
        _project = project;
        return this;
    }
    
    public DotnetExecutor UsingProjectPath(bool usePath = false)
    {
        _useProjectPath = usePath;
        return this;
    }
    
    public DotnetExecutor WithVerbosityLevel(VerbosityLevel verbosityLevel)
    {
        _verbosityLevel = verbosityLevel;
        return this;
    }
    
    public DotnetExecutor WithOutput(string output)
    {
        _output = output;
        return this;
    }
    
    public async Task DotnetRun()
    {
        var project = _useProjectPath ? $" --project {_project}" : _project;
        
        await BuildExecutor()
            .WithFileName(Commands.Dotnet)
            .WithArgs($"{Commands.DotnetRun} {project}")
            .RedirectStandardOutput(true)
            .RedirectStandardError(true)
            .UseShellExecute(false)
            .CreateNoWindow()
            .WithOutputHandler(Console.WriteLine)
            .WithErrorHandler(Console.WriteLine)
            .StartProcess();
    }
    
    public async Task DotnetBuild()
    {
        var project = _useProjectPath ? $" --project {_project}" : _project;
        var output = _useProjectPath ? $" --output {_output}" : "";
        
        await BuildExecutor()
            .WithFileName(Commands.Dotnet)
            .WithArgs($"{Commands.DotnetBuild} {project} {GetVerbosityLevel()} {output}")
            .RedirectStandardOutput(true)
            .RedirectStandardError(true)
            .UseShellExecute(false)
            .CreateNoWindow()
            .WithOutputHandler(Console.WriteLine)
            .WithErrorHandler(Console.WriteLine)
            .StartProcess();
    }
    
    public async Task DotnetPublish()
    {
        var project = _useProjectPath ? $" --project {_project}" : _project;
        var output = _useProjectPath ? $" --output {_output}" : "";
        
        await BuildExecutor()
            .WithFileName(Commands.Dotnet)
            .WithArgs($"{Commands.DotnetBuild} {project} {GetVerbosityLevel()} {output}")
            .RedirectStandardOutput(true)
            .RedirectStandardError(true)
            .UseShellExecute(false)
            .CreateNoWindow()
            .WithOutputHandler(Console.WriteLine)
            .WithErrorHandler(Console.WriteLine)
            .StartProcess();
    }
    
    public new DotnetExecutor WithWorkingDirectory(string workingDirectory)
    {
        return (base.WithWorkingDirectory(workingDirectory) as DotnetExecutor);
    }
    
    private string GetVerbosityLevel() => _verbosityLevel switch
    {
        VerbosityLevel.Quiet => "--verbosity q",
        VerbosityLevel.Minimal => "--verbosity m",
        VerbosityLevel.Normal => "--verbosity n",
        VerbosityLevel.Detailed => "--verbosity d",
        _ => "--verbosity q"
    };
}