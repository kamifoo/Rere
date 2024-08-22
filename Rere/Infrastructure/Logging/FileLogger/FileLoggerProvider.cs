namespace Rere.Infrastructure.Logging.FileLogger;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _logFilePath;
    private readonly string _logDateTime;

    public FileLoggerProvider(string logFilePath = "logs/Default")
    {
        _logDateTime = DateTime.UtcNow.ToString("s");
        _logFilePath = $"{logFilePath}/{_logDateTime}.rere.api.log";
        EnsureLogFileExists();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger<object>(_logFilePath, categoryName);
    }

    public void Dispose()
    {
    }

    private void EnsureLogFileExists()
    {
        using var fileStream = FileStreamProvider.Of(_logFilePath);
        using var streamWriter = new StreamWriter(fileStream);

        streamWriter.WriteLine("=== Rere LOG START ===");
        streamWriter.WriteLine($"Created at: {_logDateTime} (UTC)");
        streamWriter.WriteLine("=====================================");
        streamWriter.Flush();
    }
}

public static class FileStreamProvider
{
    public static FileStream Of(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if (directoryPath != null && !Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        return new FileStream(filePath, FileMode.Append);
    }
}