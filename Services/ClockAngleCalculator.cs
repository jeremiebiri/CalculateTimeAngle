namespace CalculateTimeAngle.Services
{
  public class ClockAngleCalculator : IClockAngleCalculator
  {
    public double CalculateTotalDegres(int hour, int minute)
    {
      int h = hour % 12;
      double hourDeg = h * 30 + .5 * minute;
      double minuteDeg = minute * 6;

      return hourDeg + minuteDeg;
    }
  }
}
