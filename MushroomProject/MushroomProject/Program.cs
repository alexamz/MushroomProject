using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
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
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.LogInformation(e, "Weather Tracker stopped due to an exception");
                exitCode = -1;
                throw;
            }
            finally
            {
                logger.LogInformation("Closing Weather Tracker...");
            }

            Environment.Exit(exitCode);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
                .ConfigureServices(serviceCollection => serviceCollection
                    .Configure<AzureFileLoggerOptions>(options =>
                    {
                        options.FileName = "azure-diagnostics-";
                        options.FileSizeLimit = 50 * 1024;
                        options.RetainedFileCountLimit = 5;
                    })
                    .Configure<AzureBlobLoggerOptions>(options =>
                    {
                        options.BlobName = "log.txt";
                    }))

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<AzureFileLoggerOptions>(options =>
                    {
                        options.FileName = "azure-diagnostics-";
                        options.FileSizeLimit = 50 * 1024;
                        options.RetainedFileCountLimit = 5;
                    });
                    services.Configure<AzureBlobLoggerOptions>(options =>
                        {
                            options.BlobName = "log.txt";
                        });
                }
                );

        private static void FixCurrentDirectory() =>
              Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(Program).Assembly.Location));
    }
}
