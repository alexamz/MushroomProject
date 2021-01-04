using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database
{
    public class AppSettingsConfiguration
    {
        public MushroomsDatabaseSettings MushroomsDatabaseSettings { get; set; }
        public OpenWeatherMap OpenWeatherMap { get; set; }
    }

    public class OpenWeatherMap
    {
        public string ApiKey { get; set; }
    }

    public class MushroomsDatabaseSettings
    {
        public string WeatherCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
