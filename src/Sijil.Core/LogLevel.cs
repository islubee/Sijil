namespace Sijil;

/// <summary>
/// Severity level of a log event, ordered from least to most severe.
/// </summary>
public enum LogLevel : byte
{
    /// <summary>Fine-grained diagnostic information, typically disabled in production.</summary>
    Trace = 0,

    /// <summary>Diagnostic information useful during development.</summary>
    Debug = 1,

    /// <summary>Normal application flow and state changes.</summary>
    Information = 2,

    /// <summary>Unexpected situations that do not prevent the application from continuing.</summary>
    Warning = 3,

    /// <summary>Errors that caused an operation to fail, but are recoverable.</summary>
    Error = 4,

    /// <summary>Unrecoverable failures; the application may terminate.</summary>
    Fatal = 5
}
