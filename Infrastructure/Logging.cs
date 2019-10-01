using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Benchmark.Infrastructure
{
    internal static class Logging
    {
        public static void ConfigureLogging(this IServiceCollection services)
        {
            services.AddLogging(
               opt =>
               {
                   var serilogConfiguration = new LoggerConfiguration()
                       .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                       .MinimumLevel.Verbose();

                   Log.Logger = serilogConfiguration.CreateLogger();
                   opt.AddSerilog(dispose: true);
               });
        }
    }
}
