using Serilog;
using Serilog.Events;

namespace Api.Configurations
{
    public static class LoggingConfiguration
    {
        public static void AddLoggingConfiguration(this WebApplicationBuilder builder)
        {
            Log.Logger = new Serilog.LoggerConfiguration()
                // 🔹 Nível padrão
                .MinimumLevel.Information()

                // 🔇 Silenciar barulho de framework
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)

                .Enrich.FromLogContext()

                .WriteTo.Console(
                    outputTemplate:
                    "[{Level:u3}] {Message:lj} (TraceId={TraceId}){NewLine}{Exception}"
                )

                .WriteTo.File(
                    path: "Logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate:
                    "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj} (TraceId={TraceId}){NewLine}{Exception}"
                )

                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
