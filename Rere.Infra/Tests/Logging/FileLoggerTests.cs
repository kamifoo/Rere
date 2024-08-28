using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Rere.Infra.Logging.FileLogger;

namespace Rere.Infra.Tests.Logging;

[TestFixture]
public class FileLoggerTests
{
    private const string LogFileName = "test_log.txt";
    private string _logFilePath;
    private FileLogger<FileLoggerTests> _logger;

    [SetUp]
    public void Setup()
    {
        _logFilePath = Directory.GetCurrentDirectory() + '/' + LogFileName;
        _logger = new FileLogger<FileLoggerTests>(_logFilePath, "FileLoggerTests");
    }

    [TearDown]
    public void Cleanup()
    {
        if (File.Exists(_logFilePath)) File.Delete(_logFilePath);
    }

    [Test]
    public void Log_WritesToLogFile()
    {
        var logMessage = "Test log message";
        var logLevel = LogLevel.Information;
        var eventId = new EventId(1, "TestEvent");

        _logger.Log(logLevel, eventId, logMessage, null, (state, ex) => state.ToString());

        File.Exists(_logFilePath).Should().BeTrue();

        var logContents = File.ReadAllText(_logFilePath);
        logContents.Should().Contain(logMessage);
    }

    [Test]
    public void BeginScope_CreatesNewScope()
    {
        var scopeMessage = "TestScope";

        using var scope = _logger.BeginScope(scopeMessage);

        scope.Should().NotBeNull();
        scope!.ToString().Should().Contain(scopeMessage);
    }
}