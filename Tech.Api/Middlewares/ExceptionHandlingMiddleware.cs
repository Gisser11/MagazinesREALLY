using System.Net;
using Tech.Domain.Enum;
using Tech.Domain.Result;
using ILogger = Serilog.ILogger;

namespace Tech.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(ILogger logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContent)
    {
        try
        {
                await _next(httpContent);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContent, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.Error(exception, exception.Message);

        var response = exception switch
        {
            UnauthorizedAccessException _ => new BaseResult() {ErrorMessage = exception.Message, ErrorCode = (int)ErrorCode.UserNotFound},
            _ => new BaseResult(){ErrorMessage = "Internal Server ЭРОР! ПЛИЗ РЕТРУ ЛАТЕР", ErrorCode = (int)ErrorCode.InternalServerError}
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)response.ErrorCode;

        await httpContext.Response.WriteAsJsonAsync(response);
    }
}