using CalculateTimeAngle.Models;
using CalculateTimeAngle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculateTimeAngle.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CalculateTimeAngleApiController : ControllerBase
  {
    private readonly IClockAngleCalculator _calculator;
    private readonly ILogger<CalculateTimeAngleApiController> _logger;
  
   

    public CalculateTimeAngleApiController(IClockAngleCalculator calculator, ILogger<CalculateTimeAngleApiController> logger)
    {
      _calculator = calculator;
      _logger = logger;
    }



    [HttpGet("calculate")]
    [ProducesResponseType(typeof(TimeAngleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]

    public IActionResult Calculate(string time, [FromQuery] TimeAngleRequest request)
    {
      if (request == null || (string.IsNullOrEmpty(request.Time) && (request.Hour == null || request.Minute == null)))
      {
        return BadRequest("Invalid request format. Please provide the correct format.");
      }
      int hour = 0;
      int minute = 0;
      if (!string.IsNullOrEmpty(request.Time))
      {
        var timeParts = request.Time.Split(':');
        if (timeParts.Length != 2 || !int.TryParse(timeParts[0], out hour) || !int.TryParse(timeParts[1], out minute))
        {
          return BadRequest("Invalid time format. Please use 'HH:mm'.");
        }
      }
      else
      {
        hour = request.Hour.Value;
        minute = request.Minute.Value;
      }
      if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
      {
        return BadRequest("Hour must be between 0 and 23, and minute must be between 0 and 59.");
      }
      double totalDegrees = _calculator.CalculateTotalDegres(hour, minute);
      var response = new TimeAngleResponse { TotalDegrees = totalDegrees };
      return Ok(response);
    }


  }
}
