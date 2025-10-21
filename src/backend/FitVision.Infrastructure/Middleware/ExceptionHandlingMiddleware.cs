using FitVision.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ValidationException = FitVision.Application.Exceptions.ValidationException;

namespace FitVision.Infrastructure.Middleware
{
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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error");
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error");
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unhandled exception");
                await HandleExceptionAsync(context, "An unexpected error occurred.", HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new { error = message, statusCode = (int)statusCode };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
