using System.Collections.Concurrent;

namespace LootDumpProcessor.Logger;

public class QueueLogger : ILogger
{
    private BlockingCollection<LoggedMessage> queuedMessages = new BlockingCollection<LoggedMessage>();
    private Task? loggerThread;
    private bool isRunning;
    private int logLevel;
    private static readonly int _logTerminationTimeoutMs = 1000;
    private static readonly int _logTerminationRetryCount = 3;

    public void Setup()
    {
        SetLogLevel();
        isRunning = true;
        loggerThread = Task.Factory.StartNew(() =>
        {
            while (isRunning)
            {
                while (queuedMessages.TryTake(out var value))
                {
                    Console.ResetColor();
                    switch (value.LogLevel)
                    {
                        case LogLevel.Error:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case LogLevel.Warning:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case LogLevel.Debug:
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;
                        case LogLevel.Info:
                        default:
                            break;
                    }

                    Console.WriteLine(value.Message);
                }

                Thread.Sleep(
                    TimeSpan.FromMilliseconds(LootDumpProcessorContext.GetConfig().LoggerConfig.QueueLoggerPoolingTimeoutMs));
            }
        }, TaskCreationOptions.LongRunning);
    }

    private void SetLogLevel()
    {
        logLevel = GetLogLevel(LootDumpProcessorContext.GetConfig().LoggerConfig.LogLevel);
    }

    private int GetLogLevel(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Error:
                return 1;
            case LogLevel.Warning:
                return 2;
            case LogLevel.Info:
                return 3;
            case LogLevel.Debug:
                return 4;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Log(string message, LogLevel level)
    {
        if (GetLogLevel(level) <= logLevel)
            queuedMessages.Add(new LoggedMessage { Message = message, LogLevel = level });
    }

    // Wait for graceful termination of the logging thread
    public void Stop()
    {
        isRunning = false;
        if (loggerThread != null)
        {
            Console.ResetColor();
            int retryCount = 0;
            while (!loggerThread.IsCompleted)
            {
                if (retryCount == _logTerminationRetryCount)
                {
                    Console.WriteLine(
                        $"Logger thread did not terminate by itself after {retryCount} retries. Some log messages may be lost.");
                    break;
                }

                Console.WriteLine($"Waiting {_logTerminationTimeoutMs}ms for logger termination");
                Thread.Sleep(_logTerminationTimeoutMs);
                retryCount++;
            }
        }
    }

    class LoggedMessage
    {
        public string Message { get; init; }
        public LogLevel LogLevel { get; init; }
    }
}