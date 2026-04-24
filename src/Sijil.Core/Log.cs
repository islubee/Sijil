using System;
using System.Threading;

namespace Sijil;

/// <summary>
/// Global, static logging facade. Provides one-line logging from anywhere
/// in the application without dependency injection.
/// </summary>
/// <remarks>
/// <para>
/// The static facade is intended for application code where DI is impractical
/// or where a single global logger is acceptable (CLI tools, scripts, quick samples).
/// For testable, per-class logging, inject <see cref="ILogger"/> via DI instead.
/// </para>
/// <para>
/// Call <see cref="LogManager.Configure"/> once at application startup before
/// using any logging methods. Events written before configuration are silently dropped.
/// </para>
/// </remarks>
public static class Log
{
    private static ILogger s_logger = NullLogger.Instance;

    /// <summary>Gets or sets the underlying logger. Set by <see cref="LogManager"/> during configuration.</summary>
    public static ILogger Logger
    {
        get => Volatile.Read(ref s_logger);
        internal set => Volatile.Write(ref s_logger, value ?? NullLogger.Instance);
    }

    /// <summary>Writes a Trace-level event.</summary>
    public static void Trace(string message, object? properties = null)
        => Logger.Write(LogLevel.Trace, message, properties);

    /// <summary>Writes a Debug-level event.</summary>
    public static void Debug(string message, object? properties = null)
        => Logger.Write(LogLevel.Debug, message, properties);

    /// <summary>Writes an Information-level event.</summary>
    public static void Info(string message, object? properties = null)
        => Logger.Write(LogLevel.Information, message, properties);

    /// <summary>Writes an Information-level event.</summary>
    public static void Information(string message, object? properties = null)
        => Logger.Write(LogLevel.Information, message, properties);

    /// <summary>Writes a Warning-level event.</summary>
    public static void Warn(string message, object? properties = null)
        => Logger.Write(LogLevel.Warning, message, properties);

    /// <summary>Writes a Warning-level event.</summary>
    public static void Warning(string message, object? properties = null)
        => Logger.Write(LogLevel.Warning, message, properties);

    /// <summary>Writes an Error-level event.</summary>
    public static void Error(string message, object? properties = null)
        => Logger.Write(LogLevel.Error, message, properties);

    /// <summary>Writes an Error-level event with an exception.</summary>
    public static void Error(Exception exception, string message, object? properties = null)
        => Logger.Write(LogLevel.Error, exception, message, properties);

    /// <summary>Writes a Fatal-level event.</summary>
    public static void Fatal(string message, object? properties = null)
        => Logger.Write(LogLevel.Fatal, message, properties);

    /// <summary>Writes a Fatal-level event with an exception.</summary>
    public static void Fatal(Exception exception, string message, object? properties = null)
        => Logger.Write(LogLevel.Fatal, exception, message, properties);

    /// <summary>Returns a child logger that automatically includes the given properties on every event.</summary>
    public static ILogger ForContext(object properties) => Logger.ForContext(properties);
}

/// <summary>A logger implementation that silently discards all events.</summary>
internal sealed class NullLogger : ILogger
{
    public static readonly NullLogger Instance = new();
    private NullLogger() { }

    public bool IsEnabled(LogLevel level) => false;
    public void Write(LogLevel level, string message, object? properties = null) { }
    public void Write(LogLevel level, Exception? exception, string message, object? properties = null) { }
    public ILogger ForContext(object properties) => this;
}
