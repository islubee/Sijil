using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sijil;

/// <summary>
/// Represents a destination for log events. Implementations must be thread-safe.
/// </summary>
/// <remarks>
/// Sinks receive events in batches from the internal pipeline. Implementations should
/// handle transient failures internally and never throw unhandled exceptions — doing so
/// may cause events to be lost or the host application to be destabilized.
/// </remarks>
public interface ILogSink : IAsyncDisposable
{
    /// <summary>
    /// Writes a batch of events to the destination.
    /// </summary>
    /// <param name="events">The batch to write. Never null, never empty.</param>
    /// <param name="cancellationToken">Cancels the write operation.</param>
    Task EmitBatchAsync(IReadOnlyList<LogEvent> events, CancellationToken cancellationToken);

    /// <summary>
    /// Flushes any buffered state to the destination. Called on graceful shutdown.
    /// </summary>
    Task FlushAsync(CancellationToken cancellationToken);
}
