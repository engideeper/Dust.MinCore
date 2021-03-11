using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Dust.MinCore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Information()
                             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                             .Enrich.FromLogContext()
                             .WriteTo.Console()
                             .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                             .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Ending web host");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //Çå³ýÄ¬ÈÏÅäÖÃ
                    config.Sources.Clear();
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
