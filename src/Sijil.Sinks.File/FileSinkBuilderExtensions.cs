using System;

namespace Sijil.Sinks.File;

/// <summary>Configuration for the file sink.</summary>
public sealed class FileSinkOptions
{
    /// <summary>Path to the log file. Supports date substitution via rolling.</summary>
    public string Path { get; set; } = "logs/log-.txt";

    /// <summary>When true, writes events as one JSON object per line.</summary>
    public bool Json { get; set; } = true;

    /// <summary>How frequently to roll to a new file.</summary>
    public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

    /// <summary>Maximum size in bytes for a single file before it rolls. Null disables size-based rolling.</summary>
    public long? FileSizeLimitBytes { get; set; } = 100 * 1024 * 1024; // 100 MB

    /// <summary>Number of rolled files to retain. Older files are deleted.</summary>
    public int? RetainedFileCountLimit { get; set; } = 31;
}

/// <summary>Rolling frequency for the file sink.</summary>
public enum RollingInterval
{
    /// <summary>Never roll on time boundaries (size-based only).</summary>
    Infinite,
    /// <summary>Roll each year.</summary>
    Year,
    /// <summary>Roll each month.</summary>
    Month,
    /// <summary>Roll each day.</summary>
    Day,
    /// <summary>Roll each hour.</summary>
    Hour,
    /// <summary>Roll each minute.</summary>
    Minute
}

/// <summary>Extension methods for configuring the file sink.</summary>
public static class FileSinkBuilderExtensions
{
    /// <summary>Adds a rolling file sink to the pipeline.</summary>
    public static LoggerBuilder UseFile(this LoggerBuilder builder, string path)
        => builder.UseFile(o => o.Path = path);

    /// <summary>Adds a rolling file sink with custom options.</summary>
    public static LoggerBuilder UseFile(this LoggerBuilder builder, Action<FileSinkOptions> configure)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);
#else
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        if (configure is null) throw new ArgumentNullException(nameof(configure));
#endif

        var options = new FileSinkOptions();
        configure(options);

        // TODO (Phase 1): Implement FileSink : ILogSink wrapping Serilog.Sinks.File
        //                 and register via builder.AddSink(new FileSink(options));
        return builder;
    }
}