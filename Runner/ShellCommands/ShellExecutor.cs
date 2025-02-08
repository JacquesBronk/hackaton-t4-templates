using System.Diagnostics;

namespace Runner.ShellCommands;

public class ShellExecutor
{
    private Process _process;
    private string _fileName;
    private string _args = "";
    private bool _redirectStandardOutput;
    private bool _redirectStandardError;
    private bool _useShellExecute;
    private bool _createNoWindow;
    private string _workingDirectory;
    private Action<string> _outputHandler = Console.WriteLine;
    private Action<string> _errorHandler = Console.WriteLine;
    
    
    public static ShellExecutor BuildExecutor() => new ShellExecutor();
    
    public ShellExecutor WithFileName(string fileName)
    {
        _fileName = fileName;
        return this;
    }
    
    public ShellExecutor WithArgs(string args)
    {
        _args = args;
        return this;
    }
    
    public ShellExecutor RedirectStandardOutput(bool redirectStandardOutput = false)
    {
        _redirectStandardOutput = redirectStandardOutput;
        return this;
    }
    
    public ShellExecutor RedirectStandardError(bool redirectStandardError = false)
    {
        _redirectStandardError = redirectStandardError;
        return this;
    }
    
    public ShellExecutor UseShellExecute(bool shellExecute = true)
    {
        _useShellExecute = shellExecute;
        return this;
    }
    
    public ShellExecutor CreateNoWindow(bool createNoWindow = true)
    {
        _createNoWindow = createNoWindow;
        return this;
    }
    
    public ShellExecutor WithOutputHandler(Action<string> outputHandler)
    {
        _outputHandler = outputHandler;
        return this;
    }
    
    public ShellExecutor WithErrorHandler(Action<string> errorHandler)
    {
        _errorHandler = errorHandler;
        return this;
    }
    
    public ShellExecutor WithWorkingDirectory(string workingDirectory)
    {
        _workingDirectory = workingDirectory;
        return this;
    }
    
    public async Task StartProcess()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = _fileName,
            Arguments = _args,
            RedirectStandardOutput = _redirectStandardOutput,
            RedirectStandardError = _redirectStandardError,
            UseShellExecute = _useShellExecute,
            CreateNoWindow = _createNoWindow
        };
        
        if (!string.IsNullOrWhiteSpace(_workingDirectory))
        {
            startInfo.WorkingDirectory = _workingDirectory;
        }
        
        _process = new Process
        {
            StartInfo = startInfo
        };
        
        _process.Start();
        
        var outputTask = _process.StandardOutput.ReadToEndAsync();
        var errorTask = _process.StandardError.ReadToEndAsync();

        await _process.WaitForExitAsync();

        string output = await outputTask;

        _outputHandler("Output:");
        _outputHandler(output);

        string error = await errorTask;

        if (!string.IsNullOrWhiteSpace(error))
        {
            _errorHandler("Error:");
            _errorHandler(error);
        }
    }
}