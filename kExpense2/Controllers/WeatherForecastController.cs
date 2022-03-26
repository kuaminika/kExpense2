using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kExpense2.kConfigs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace kExpense2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private IConfiguration _tconfig;
        private KxtensionsConfig _config;
        private readonly ILogger<WeatherForecastController> _logger;

        /*
         private KxtensionsConfig _config;
        private KExpenseToolBox toolBox;
        public ExpensesController(IConfiguration Configuration)
        {
            _config = new KxtensionsConfig( Configuration);
            toolBox = new KExpenseToolBox(_config);
        }

         */

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _tconfig = configuration;
            _config = new KxtensionsConfig(configuration);
            _logger = logger;
        }


        [Route("getThings")]
        public string GetThings()
        {
            return $"{_tconfig["theBest"]}-->{ _config.Get("theBest")}";
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Log(LogLevel.Trace, "inside the get");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary =  $"{_tconfig["theBest"]}-->{ _config.Get("theBest")}"+"--" + Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
