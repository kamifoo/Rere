namespace Rere.Infrastructure.Logging.FileLogger;

public class FileLogger<T>(string filePath, string categoryName) : ILogger<T>
{
    private readonly StreamWriter _writer = new(FileStreamProvider.Of(filePath));

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new LogScope(this, state?.ToString() ?? string.Empty);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);
        var logMessage = $"{DateTime.UtcNow:s}: [{categoryName}] {logLevel}: {message}";

        _writer.WriteLine(logMessage);
        _writer.Flush();
    }

    private static readonly AsyncLocal<LogScope?> CurrentScope = new();

    private class LogScope : IDisposable
    {
        private readonly FileLogger<T> _logger;
        private readonly LogScope? _parent;
        private readonly string _state;

        public LogScope(FileLogger<T> logger, string state)
        {
            _logger = logger;
            _state = state;
            _parent = CurrentScope.Value;
            CurrentScope.Value = this;
        }

        public void Dispose()
        {
            CurrentScope.Value = _parent;
        }

        public override string ToString()
        {
            return _parent == null ? _state : $"{_parent.ToString()} => {_state}";
        }
    }
}