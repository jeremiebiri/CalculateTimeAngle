namespace CalculateTimeAngle.Middleware
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
      _logger = logger;
      _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {

        _logger.LogError(ex, "An unexpected/unhandler error occurred.");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
        // ToDO [*]try to conver response to JSON during refactoring
      }
    }
  }
}
