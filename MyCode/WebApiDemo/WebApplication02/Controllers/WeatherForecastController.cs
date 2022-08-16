using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore.Extensions;

namespace WebApplication02.Controllers
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
        private readonly ILog _log4NetLogger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILog log4netLogger)
        {
            _logger = logger;
            _log4NetLogger = log4netLogger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Logger.Get");
            _logger.LogDebug("Debug");
            _logger.LogTrace("Trace");
            _log4NetLogger.Debug("Log4net.Debug");
            _log4NetLogger.Trace("Log4net.Trace",null);
            _log4NetLogger.Info("Log4net.Get");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
