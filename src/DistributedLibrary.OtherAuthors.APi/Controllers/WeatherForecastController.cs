using DistributedLibrary.Authors.Api.Models;
using DistributedLibrary.Authors.Api.Services;
using DistributedLibrary.Books.Service.Services;
using DistributedLibrary.OtherAuthors.APi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.OtherAuthors.APi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IOtherService _dataService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , IOtherService dataService)
        {

            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        public ActionResult<IEnumerable<Autores>> GetEntityById(int Id)
        {

            var result = _dataService.GetEntityById(Id);
            return Ok(result);

        }



        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
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
