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

        public MessageHandler(WeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            this.cancellationToken = stoppingToken;
            string uri = $"https://api.openweathermap.org/data/2.5/weather?lat=41.609935&lon=2.029882&lang=es&appid=ca5dfdf34c56854aced66dbfc1e471b0";

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
                    weatherService.Create(JsonSerializer.Deserialize<WeatherResponse>(GetWeatherData(uri).Result););
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                await Task.Delay(TimeSpan.FromMinutes(minutes), cancellationToken);
            }
        }

        private static async Task<string> GetWeatherData(string uri)
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
                Console.WriteLine(e);
            }

            return null;
        }
    }
}