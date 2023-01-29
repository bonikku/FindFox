using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<Exception> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<Exception> logger, IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;

    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, exc.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var res = _env.IsDevelopment()
          ? new ApiException(context.Response.StatusCode, exc.Message, exc.StackTrace?.ToString())
          : new ApiException(context.Response.StatusCode, exc.Message, "Internal Server Error");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(res, options);

        await context.Response.WriteAsync(json);
      }

    }
  }
}