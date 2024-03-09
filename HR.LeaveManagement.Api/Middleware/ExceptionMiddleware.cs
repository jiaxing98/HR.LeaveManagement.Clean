using HR.LeaveManagement.Api.Models;
using Newtonsoft.Json;
using System.Net;
using BadRequestException = HR.LeaveManagement.Application.Exceptions.BadRequestException;
using NotFoundException = HR.LeaveManagement.Application.Exceptions.NotFoundException;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomExceptionDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomExceptionDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(BadRequestException),
                        Detail = badRequest.InnerException?.Message,
                        Errors = badRequest.ValidationErrors
                    };
                    break;
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomExceptionDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notFound.InnerException?.Message,
                    };
                    break;
                default:
                    problem = new CustomExceptionDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.InnerException?.Message,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problem, Formatting.Indented);
            _logger.LogError(ex, logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
