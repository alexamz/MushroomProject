using Domain;
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
            List<string> data = new List<string>();
            while (!cancellationToken.IsCancellationRequested)
            {
                data.Add(GetWeatherData(uri).Result);
                await Task.Delay(TimeSpan.FromMinutes(minutes), cancellationToken);
            }
        }

        public async Task<string> GetWeatherData(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}