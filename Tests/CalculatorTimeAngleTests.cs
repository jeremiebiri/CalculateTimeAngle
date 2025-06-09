using CalculateTimeAngle.Controllers;
using CalculateTimeAngle.Models;
using CalculateTimeAngle.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CalculateTimeAngle.Tests
{
  public class CalculatorTimeAngleTests
  {
    private readonly IClockAngleCalculator _calculator;
    public CalculatorTimeAngleTests()
    {
      _calculator = new ClockAngleCalculator();
    }
    [Theory]
    [InlineData(3, 0, 90)]
    [InlineData(6, 0, 180)]
    [InlineData(9, 0, 270)]
    [InlineData(12, 0, 0)]
    [InlineData(1, 30, 165)]
    [InlineData(2, 15, 97.5)]
    public void CalculateTotalDegrees_ShouldReturnCorrectAngle(int hour, int minute, double expected)
    {
      // Act
      double result = _calculator.CalculateTotalDegres(hour, minute);
      // Assert
      Assert.Equal(expected, result);
    }

    
    [Theory]
    [InlineData("25:00")]
    [InlineData("12:60")]
    [InlineData("invalidInput")]
    [InlineData(null)]

    public void CalculateTimeAngle_Given_InvalidTimeFormat_ShouldReturnBadRequest(string time)
    {
      
      var mockCalculator = new Mock<IClockAngleCalculator>();
      var mockLogger = new Mock<ILogger<CalculateTimeAngleApiController>>();
      var controller = new CalculateTimeAngleApiController(mockCalculator.Object, mockLogger.Object);
      var request = new TimeAngleRequest { Time = time };

      var result = controller.Calculate(time, request) as BadRequestObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
      dynamic response = result.Value!;
      Assert.NotNull(response.error);


    }




  }
}
