using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using Services;
using Services.Database;
using Services.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class MessageHandler : IMessageHandler
    {
        private CancellationToken cancellationToken;
        private readonly WeatherService weatherService;
        private readonly AppSettingsConfiguration settings;
        private readonly ILogger<MessageHandler> logger;

        public MessageHandler(WeatherService weatherService,
            AppSettingsConfiguration settings,
            ILogger<MessageHandler> logger)
        {
            this.weatherService = weatherService;
            this.settings = settings;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            this.cancellationToken = stoppingToken;
            string uri = $@"https://api.openweathermap.org/data/2.5/weather?lat=41.609935&lon=2.029882&lang=es&appid={settings.OpenWeatherMap.ApiKey}";

            await GetDataPeriodicallyAsync(uri);
        }
        public void Stop(CancellationToken stoppingToken)
        {

        }

        private async Task GetDataPeriodicallyAsync(string uri)
        {
            const int minutes = 60;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    var json = GetWeatherData(uri).Result;
                    logger.LogDebug($"Getting data from OpenWeatherMap took {sw.ElapsedMilliseconds}");
                    logger.LogDebug(json);
                    sw.Restart();
                    weatherService.Create(JsonSerializer.Deserialize<WeatherResponse>(json));
                    logger.LogDebug($"Storing in mongodb took {sw.ElapsedMilliseconds}");
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                }
                await Task.Delay(TimeSpan.FromMinutes(minutes), cancellationToken);
            }
        }

        private async Task<string> GetWeatherData(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }

            return null;
        }
    }
}