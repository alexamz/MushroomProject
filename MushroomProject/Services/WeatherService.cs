using Services.Messages;
using Services.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WeatherService
    {
        private readonly IMongoCollection<WeatherResponse> weather;

        public WeatherService(AppSettingsConfiguration settings)
        {
            var client = new MongoClient(settings.MushroomsDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(settings.MushroomsDatabaseSettings.DatabaseName);

            weather = database.GetCollection<WeatherResponse>(settings.MushroomsDatabaseSettings.WeatherCollectionName);
        }

        public List<WeatherResponse> Get() =>
            weather.Find(weather => true).ToList();

        public WeatherResponse Get(string id) =>
            weather.Find(weather => weather.Id.ToString() == id).FirstOrDefault();

        public WeatherResponse Create(WeatherResponse w)
        {
            weather.InsertOne(w);
            return w;
        }

        public void Update(string id, WeatherResponse weatherIn) =>
            weather.ReplaceOne(weather => weather.Id.ToString() == id, weatherIn);

        public void Remove(WeatherResponse weatherIn) =>
            weather.DeleteOne(weather => weather.Id == weatherIn.Id);

        public void Remove(string id) =>
            weather.DeleteOne(weather => weather.Id.ToString() == id);
    }
}
