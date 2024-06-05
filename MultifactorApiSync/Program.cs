using MultifactorApiSync;
using Serilog;
using System.Runtime.InteropServices;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Multifactor API Sync";
});

RegisterLogger(builder);

Environment.GetCommandLineArgs();

builder.Services.AddHostedService<UsersSynchronizer>();

var host = builder.Build();
host.Run();

static void RegisterLogger(HostApplicationBuilder builder)
{
    var loggerConfig = new LoggerConfiguration().WriteTo.Console();
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        loggerConfig.WriteTo.EventLog("Multifactor AD Sync", "Application", manageEventSource: false);
    }

    Log.Logger = loggerConfig.CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);
}
