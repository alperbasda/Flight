using FlightProvider.AirSearch.Service.Exceptions;
using FlightProvider.AirSearch.Service.Models.Responses;

namespace FlightProvider.AirSearch.Service.Middlewares;


public static class ExceptionHandlerMiddlewareExtension
{
    #region For Use

    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }

    #endregion
}
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Exceptionlar tiplere göre yönetilebilir.
        //Error logging işlemleri burada handle edilebilir.+
        try
        {
            await _next(context);
        }
        catch (BusinessException error)
        {
            _logger.LogInformation($"Business Error : {error.Message} StackTrace : {error.StackTrace ?? ""}");
            await context.Response.WriteAsJsonAsync(ApiResponse<MessageResponse>.Fail(error.Message, 400));
        }
        catch (Exception error)
        {
            _logger.LogError($"{error.Message} StackTrace : {error.StackTrace ?? ""}");
            await context.Response.WriteAsJsonAsync(ApiResponse<MessageResponse>.Fail("İşlem sırasında bir hata oluştu.", 500));
        }
    }

}
