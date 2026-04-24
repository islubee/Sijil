using System;

namespace Sijil;

/// <summary>
/// The primary logging abstraction. Prefer the static <see cref="Log"/> facade
/// for one-line logging; inject <see cref="ILogger"/> when testability matters.
/// </summary>
public interface ILogger
{
    /// <summary>Returns true if events at the given level would be emitted.</summary>
    bool IsEnabled(LogLevel level);

    /// <summary>Writes a log event.</summary>
    void Write(LogLevel level, string message, object? properties = null);

    /// <summary>Writes a log event with an associated exception.</summary>
    void Write(LogLevel level, Exception? exception, string message, object? properties = null);

    /// <summary>Returns a child logger that automatically includes the given properties on every event.</summary>
    ILogger ForContext(object properties);
}
