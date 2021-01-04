using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Database;
using Services.Messages;
using System.Collections.Generic;

namespace WeathersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService weatherService;

        public WeatherController(WeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public ActionResult<List<WeatherResponse>> Get() =>
            weatherService.Get();

        [HttpGet("{id:length(24)}", Name = "GetWeather")]
        public ActionResult<WeatherResponse> Get(string id)
        {
            var weather = weatherService.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        [HttpPost]
        public ActionResult<WeatherResponse> Create(WeatherResponse weather)
        {
            weatherService.Create(weather);

            return CreatedAtRoute("GetWeather", new { id = weather.Id.ToString() }, weather);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, WeatherResponse weatherIn)
        {
            var weather = weatherService.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            weatherService.Update(id, weatherIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var weather = weatherService.Get(id);

            if (weather == null)
            {
                return NotFound();
            }

            weatherService.Remove(weather.Id.ToString());

            return NoContent();
        }
    }
}