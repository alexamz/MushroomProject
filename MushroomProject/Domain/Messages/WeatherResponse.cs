using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Services.Messages
{
    public class WeatherResponse
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string User { get; set; }
        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }
        [JsonPropertyName("weather")]
        public Weather[] Weather { get; set; }
        [JsonPropertyName("main")]
        public Main Main { get; set; }
        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }
        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }
        [JsonPropertyName("rain")]
        public Rain Rain { get; set; }
        [JsonPropertyName("snow")]
        public Snow Snow { get; set; }
        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        [JsonPropertyName("sys")]
        public Sys Sys { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Coord
    {
        [JsonPropertyName("lon")]
        public decimal Lon { get; set; }
        [JsonPropertyName("lat")]
        public decimal Lat { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public decimal Feels_like { get; set; }
        [JsonPropertyName("temp_min")]
        public decimal Temp_min { get; set; }
        [JsonPropertyName("temp_max")]
        public decimal Temp_max { get; set; }
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }
        [JsonPropertyName("deg")]
        public int Deg { get; set; }
        [JsonPropertyName("gust")]
        public decimal Gust { get; set; }

    }

    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
    public class Rain
    {
        [JsonPropertyName("1h")]
        public decimal OneHour { get; set; }
        [JsonPropertyName("3h")]
        public decimal ThreeHours { get; set; }
    }

    public class Snow
    {
        [JsonPropertyName("1h")]
        public decimal OneHour { get; set; }
        [JsonPropertyName("3h")]
        public decimal ThreeHours { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Sys
    {
        [JsonPropertyName("sunrise")]
        public int Sunrise { get; set; }
        [JsonPropertyName("sunset")]
        public int Sunset { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("main")]
        public string Main { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}