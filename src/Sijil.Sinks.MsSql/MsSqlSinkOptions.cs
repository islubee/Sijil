using System;
using System.Data;

namespace Sijil.Sinks.MsSql;

/// <summary>Configuration for the SQL Server sink.</summary>
public sealed class MsSqlSinkOptions
{
    /// <summary>Required. The connection string to the log database.</summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>Schema name of the log table. Default: <c>dbo</c>.</summary>
    public string SchemaName { get; set; } = "dbo";

    /// <summary>Table name. Default: <c>Logs</c>.</summary>
    public string TableName { get; set; } = "Logs";

    /// <summary>
    /// When true, the sink will create the schema on first use if it does not exist.
    /// Set to false in enterprise environments where DBAs manage schema.
    /// </summary>
    public bool AutoCreateSchema { get; set; } = true;

    /// <summary>Command timeout for bulk inserts. Default: 30 seconds.</summary>
    public TimeSpan CommandTimeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>Isolation level for the bulk copy transaction. Default: <see cref="IsolationLevel.ReadCommitted"/>.</summary>
    public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

    /// <summary>
    /// Optional custom column mapping. Leave null to use the default schema.
    /// </summary>
    public ColumnMapping? CustomColumns { get; set; }
}

/// <summary>Maps log-event fields to SQL column names. Enables adapting to an existing table.</summary>
public sealed class ColumnMapping
{
    public string Timestamp { get; set; } = nameof(Timestamp);
    public string Level { get; set; } = nameof(Level);
    public string Message { get; set; } = nameof(Message);
    public string MessageTemplate { get; set; } = nameof(MessageTemplate);
    public string Exception { get; set; } = nameof(Exception);
    public string Properties { get; set; } = nameof(Properties);
    public string CorrelationId { get; set; } = nameof(CorrelationId);
    public string MachineName { get; set; } = nameof(MachineName);
    public string Application { get; set; } = nameof(Application);
    public string Environment { get; set; } = nameof(Environment);
}
