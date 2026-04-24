using Sijil;

LogManager.Configure()
    .WithMinimumLevel(LogLevel.Debug)
    .Enrich("Application", "Sijil.Sample.Console")
    .Build();

Log.Info("Application started", new { Version = "0.1.0" });
Log.Warn("This is a warning");

try
{
    throw new InvalidOperationException("Simulated failure");
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong", new { UserId = 42 });
}

LogManager.Shutdown();