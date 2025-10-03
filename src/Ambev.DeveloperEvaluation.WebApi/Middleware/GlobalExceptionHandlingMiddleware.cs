using System.Net;
using System.Text.Json;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

/// <summary>
/// Middleware for handling exceptions globally, logging them, and returning a standardized error response.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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

        var response = new ApiResponse
        {
            Success = false,
            Message = "An error occurred while processing your request."
        };

        switch (exception)
        {
            case DomainException domainException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Business rule violation.";
                response.Errors = new List<ValidationErrorDetail>
                {
                    new() { Error = "DomainRule", Detail = domainException.Message }
                };
                _logger.LogWarning(exception, "Business rule violation: {Message}", exception.Message);
            break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "An unexpected internal server error has occurred.";
                response.Errors = new List<ValidationErrorDetail>
                {                    
                    new() { Error = "InternalError", Detail = "An unexpected error occurred." }
                };
                _logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);
                break;
        }

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
        await context.Response.WriteAsync(jsonResponse);
    }
}