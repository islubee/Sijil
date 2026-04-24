using System;

namespace Sijil.Sinks.MsSql;

/// <summary>Extension methods for configuring the SQL Server sink.</summary>
public static class MsSqlSinkBuilderExtensions
{
    /// <summary>Adds a SQL Server sink with a connection string and default schema.</summary>
    public static LoggerBuilder UseMsSql(this LoggerBuilder builder, string connectionString)
        => builder.UseMsSql(o => o.ConnectionString = connectionString);

    /// <summary>Adds a SQL Server sink with custom options.</summary>
    public static LoggerBuilder UseMsSql(this LoggerBuilder builder, Action<MsSqlSinkOptions> configure)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);
#else
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        if (configure is null) throw new ArgumentNullException(nameof(configure));
#endif

        var options = new MsSqlSinkOptions();
        configure(options);

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("MsSqlSinkOptions.ConnectionString is required.");

        // TODO (Phase 2): Implement MsSqlSink : ILogSink using SqlBulkCopy,
        //                 wire up schema migration, and register via builder.AddSink(...).
        return builder;
    }
}