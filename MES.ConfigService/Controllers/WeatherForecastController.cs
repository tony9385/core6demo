using Microsoft.AspNetCore.Mvc;
using SqlSugar.IOC;
using SqlSugar;
using MES.ConfigService.Models;

namespace MES.ConfigService.Controllers
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
        IAreaRepository _areaRepository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAreaRepository areaRepository)
        {
            _logger = logger;
            _areaRepository = areaRepository; 
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<AreaModel> Get()
        {
            var count=_areaRepository.GetCount();

            return count.Result;
        }
    }
}