using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.IO;

namespace MushroomProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FixCurrentDirectory();

            var logger = LogManager.LoadConfiguration("NLog.xml").GetCurrentClassLogger();
            int exitCode = 0;

            try
            {
                logger.Info("Starting Weather Tracker...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Weather Tracker stopped due to an exception");
                exitCode = -1;
                throw;
            }
            finally
            {
                logger.Info("Weather Tracker...");
                LogManager.Flush();
                LogManager.Shutdown();
            }

            Environment.Exit(exitCode);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddNLog();
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
