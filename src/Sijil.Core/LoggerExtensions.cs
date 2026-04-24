using System;

namespace Sijil;

/// <summary>
/// Convenience extensions providing level-specific logging methods.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>Writes a Trace-level event.</summary>
    public static void Trace(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Trace, message, properties);

    /// <summary>Writes a Debug-level event.</summary>
    public static void Debug(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Debug, message, properties);

    /// <summary>Writes an Information-level event.</summary>
    public static void Info(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Information, message, properties);

    /// <summary>Writes an Information-level event.</summary>
    public static void Information(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Information, message, properties);

    /// <summary>Writes a Warning-level event.</summary>
    public static void Warn(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Warning, message, properties);

    /// <summary>Writes a Warning-level event.</summary>
    public static void Warning(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Warning, message, properties);

    /// <summary>Writes an Error-level event.</summary>
    public static void Error(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Error, message, properties);

    /// <summary>Writes an Error-level event with an exception.</summary>
    public static void Error(this ILogger logger, Exception exception, string message, object? properties = null)
        => logger.Write(LogLevel.Error, exception, message, properties);

    /// <summary>Writes a Fatal-level event.</summary>
    public static void Fatal(this ILogger logger, string message, object? properties = null)
        => logger.Write(LogLevel.Fatal, message, properties);

    /// <summary>Writes a Fatal-level event with an exception.</summary>
    public static void Fatal(this ILogger logger, Exception exception, string message, object? properties = null)
        => logger.Write(LogLevel.Fatal, exception, message, properties);
}
