using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Templates;

namespace OptiCompare;

public class CustomLogger
{
    private bool _isFirst = true;
    private readonly string _path;
    private readonly Logger _logger;

    public CustomLogger(string path)
    {
        _path = path;
        _logger = new LoggerConfiguration()
            .WriteTo.File(new JsonFormatter(),
                path, 
                rollingInterval:RollingInterval.Hour,
                fileSizeLimitBytes: 50000000)
            .CreateLogger();
    }

    public void Log(string message)
    {
        _logger.Information(message);
    }
}