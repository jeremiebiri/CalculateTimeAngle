using CalculateTimeAngle.Services;
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

    


  }
}
