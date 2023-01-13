using ET.Logging.Core.Abstract;
using ET.Logging.Core.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ET.Logging.Serilog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ICustomLogger _logger;

        public WeatherForecastController(ICustomLogger logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            LogDetail log = LogDetail.Create(GetType().FullName, GetType().Name, "Emir TARTAR")
                                     .AddParameter("Result", result, result.GetType().Name);

            _logger.Info(log);

            return result;
        }
    }
}