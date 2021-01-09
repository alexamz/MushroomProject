using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MushroomProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FixCurrentDirectory();
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            int exitCode = 0;
            try
            {
                logger.LogInformation("Starting Weather Tracker...");
                host.Run();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Weather Tracker stopped due to an exception");
                exitCode = -1;
                throw;
            }
            finally
            {
                logger.LogInformation("Weather Tracker...");
            }

            Environment.Exit(exitCode);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices(services =>
                    services.AddHostedService<Worker>()
                );

        private static void FixCurrentDirectory() =>
              Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(Program).Assembly.Location));
    }
}
