using System;
using System.Threading;

namespace LogFlow;

/// <summary>
/// Provides correlation-ID propagation across async boundaries. Any log event
/// emitted within a <see cref="BeginScope"/> block will carry the scope's identifier.
/// </summary>
public static class CorrelationContext
{
    private static readonly AsyncLocal<Guid?> s_current = new();

    /// <summary>The correlation ID in effect on the current async flow, or null.</summary>
    public static Guid? Current => s_current.Value;

    /// <summary>
    /// Begins a new correlation scope. Dispose the returned handle to end the scope.
    /// </summary>
    /// <param name="correlationId">The correlation ID to associate with the scope. Generated if null.</param>
    public static IDisposable BeginScope(Guid? correlationId = null)
    {
        var previous = s_current.Value;
        s_current.Value = correlationId ?? Guid.NewGuid();
        return new Scope(previous);
    }

    private sealed class Scope : IDisposable
    {
        private readonly Guid? _previous;
        private bool _disposed;

        public Scope(Guid? previous)
        {
            _previous = previous;
        }

        public void Dispose()
        {
            if (_disposed) return;
            s_current.Value = _previous;
            _disposed = true;
        }
    }
}
