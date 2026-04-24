using System;
using System.Collections.Generic;

namespace Sijil;

/// <summary>
/// Entry point for configuring the global <see cref="Log"/> facade.
/// </summary>
public static class LogManager
{
    /// <summary>Begins fluent configuration of the logger.</summary>
    public static LoggerBuilder Configure() => new();

    /// <summary>
    /// Flushes and disposes the current logger. Call before application exit
    /// to ensure buffered events are written.
    /// </summary>
    public static void Shutdown()
    {
        if (Log.Logger is IDisposable disposable)
        {
            disposable.Dispose();
        }
        Log.Logger = NullLogger.Instance;
    }
}

/// <summary>
/// Fluent builder for configuring a <see cref="ILogger"/> and its pipeline.
/// </summary>
public sealed class LoggerBuilder
{
    private readonly List<ILogSink> _sinks = new();
    private readonly Dictionary<string, object?> _enrichers = new();
    private LogLevel _minimumLevel = LogLevel.Information;
    private BatchingOptions _batching = new();

    /// <summary>Adds a sink to the pipeline. Events will be written to every registered sink.</summary>
    public LoggerBuilder AddSink(ILogSink sink)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(sink);
#else
        if (sink is null) throw new ArgumentNullException(nameof(sink));
#endif
        _sinks.Add(sink);
        return this;
    }

    /// <summary>Sets the minimum severity to emit. Events below this level are dropped at the source.</summary>
    public LoggerBuilder WithMinimumLevel(LogLevel level)
    {
        _minimumLevel = level;
        return this;
    }

    /// <summary>Adds a static property to every event (e.g., application name, environment).</summary>
    public LoggerBuilder Enrich(string propertyName, object? value)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            throw new ArgumentException("Property name is required.", nameof(propertyName));
        _enrichers[propertyName] = value;
        return this;
    }

    /// <summary>Configures the batching behavior used to deliver events to sinks.</summary>
    public LoggerBuilder WithBatching(Action<BatchingOptions> configure)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(configure);
#else
        if (configure is null) throw new ArgumentNullException(nameof(configure));
#endif
        configure(_batching);
        return this;
    }

    /// <summary>
    /// Finalizes configuration and installs the resulting logger as the global <see cref="Log.Logger"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1822:Mark members as static",
        Justification = "Will use instance state once the pipeline is implemented in Phase 1.")]
    public ILogger Build()
    {
        // TODO (Phase 1): Construct the real pipeline wiring up Serilog + custom sinks.
        // For now, return a NullLogger so the scaffold compiles end-to-end.
        var logger = NullLogger.Instance;
        Log.Logger = logger;
        return logger;
    }

    internal IReadOnlyList<ILogSink> Sinks => _sinks;
    internal IReadOnlyDictionary<string, object?> Enrichers => _enrichers;
    internal LogLevel MinimumLevel => _minimumLevel;
    internal BatchingOptions Batching => _batching;
}

/// <summary>Controls how events are buffered before delivery to sinks.</summary>
public sealed class BatchingOptions
{
    /// <summary>Maximum number of events per batch. Default: 1000.</summary>
    public int BatchSizeLimit { get; set; } = 1000;

    /// <summary>Maximum time to wait before flushing a partial batch. Default: 2 seconds.</summary>
    public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>Maximum events held in the internal queue before backpressure kicks in. Default: 10,000.</summary>
    public int QueueLimit { get; set; } = 10_000;

    /// <summary>Action taken when the queue is full. Default: drop new events.</summary>
    public BackpressureStrategy WhenQueueFull { get; set; } = BackpressureStrategy.DropNewest;
}

/// <summary>Strategy for handling the queue when it reaches its limit.</summary>
public enum BackpressureStrategy
{
    /// <summary>Drop incoming events silently.</summary>
    DropNewest,

    /// <summary>Drop the oldest queued event to make room.</summary>
    DropOldest,

    /// <summary>Block the caller until space is available. Use with caution.</summary>
    Block
}