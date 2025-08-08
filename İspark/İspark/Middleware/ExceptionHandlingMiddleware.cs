using System;
using System.Threading.Tasks;
using İspark.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; 
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        ErrorDetails errorDetails;
        switch (exception)
        {
            case MySqlException:
                errorDetails = new ErrorDetails(StatusCodes.Status500InternalServerError, ErrorCodes.DbConnectionFailed);
                _logger.LogError(exception, "[ERROR {ErrorCode}] Database error. Path: {Path}", errorDetails.ErrorCode, context.Request.Path);
                break;
            case SecurityTokenExpiredException:
                errorDetails = new ErrorDetails(StatusCodes.Status401Unauthorized, ErrorCodes.TokenExpired);
                _logger.LogWarning("[WARN {ErrorCode}] Token expired. Path: {Path}", errorDetails.ErrorCode, context.Request.Path);
                break;
            case SecurityTokenException:
                errorDetails = new ErrorDetails(StatusCodes.Status401Unauthorized, ErrorCodes.TokenInvalid);
                _logger.LogWarning("[WARN {ErrorCode}] Invalid token. Path: {Path}", errorDetails.ErrorCode, context.Request.Path);
                break;
            default:
                errorDetails = new ErrorDetails(StatusCodes.Status500InternalServerError, ErrorCodes.InternalServerError);
                _logger.LogError(exception, "[FATAL {ErrorCode}] Unhandled exception. Path: {Path}", errorDetails.ErrorCode, context.Request.Path);
                break;
        }
        context.Response.StatusCode = errorDetails.StatusCode;
        await context.Response.WriteAsync(errorDetails.ToString());
    }
}