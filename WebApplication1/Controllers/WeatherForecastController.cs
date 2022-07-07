using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private IMyDependency _dependency;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMyDependency dependency)
        {
            _logger = logger;
            _dependency = dependency;   
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _dependency.WriteMessage($"{DateTime.Now} Get");


            _logger.LogError("Error ===");

            _logger.LogDebug("debug ===");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        private static int index = 1;

        [HttpPost(Name = "GetWeatherForecast")]
        public bool Save(List<SignalModel> info)
        {
            try
            {
                Task.Factory.StartNew(() => { Thread.Sleep(1000); });
                _logger.LogInformation("");

                foreach (var item in info)
                {
                    _logger.LogInformation($"{DateTime.Now} {index} {item.BatchIndex} {item.SignalId} {item.SignalValue} {item.ReceivedTime}");
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }

            index++;
            return true;    

        }
    }
}