using Microsoft.AspNetCore.Mvc;
using VroomVroom.Server.Data;
using VroomVroom.Server.Models;

namespace VroomVroom.Server.Controllers
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
        private readonly RentalContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RentalContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToArray();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //    {
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //        TemperatureC = Random.Shared.Next(-20, 55),
            //        Summary = 
            //    });
            //    .ToArray();
        }
    }
}
