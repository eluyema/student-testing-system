using Microsoft.AspNetCore.Mvc;

namespace student_testing_system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetUserString", Name = "GetUserString")]
        public string GetUserString()
        {
            return "User String";
        }

        [HttpGet("GetTeacherString", Name = "GetTeacherString")]
        public string GetTeacherString()
        {
            return "Teacher String";
        }
    }
}