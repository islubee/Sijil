using System;
using System.Collections.Generic;

namespace Sijil;

/// <summary>
/// An immutable representation of a single log event as it flows through the pipeline.
/// </summary>
public sealed class LogEvent
{
    /// <summary>Creates a new log event.</summary>
    public LogEvent(
        DateTimeOffset timestamp,
        LogLevel level,
        string message,
        string? messageTemplate = null,
        Exception? exception = null,
        IReadOnlyDictionary<string, object?>? properties = null)
    {
        Timestamp = timestamp;
        Level = level;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        MessageTemplate = messageTemplate;
        Exception = exception;
        Properties = properties ?? EmptyProperties;
    }

    private static readonly IReadOnlyDictionary<string, object?> EmptyProperties =
        new Dictionary<string, object?>(0);

    /// <summary>When the event occurred.</summary>
    public DateTimeOffset Timestamp { get; }

    /// <summary>Severity of the event.</summary>
    public LogLevel Level { get; }

    /// <summary>Rendered, human-readable message.</summary>
    public string Message { get; }

    /// <summary>Original message template before property substitution, if any.</summary>
    public string? MessageTemplate { get; }

    /// <summary>Exception associated with the event, if any.</summary>
    public Exception? Exception { get; }

    /// <summary>Structured properties attached to the event.</summary>
    public IReadOnlyDictionary<string, object?> Properties { get; }
}
