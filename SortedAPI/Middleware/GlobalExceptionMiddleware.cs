using Sorted.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace SortedAPI.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                message = "Invalid request";
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = "No readings found for the specified stationId";
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(ValidationException))
            {
                message = "Invalid request parameter";
                status = HttpStatusCode.BadRequest;
            }
            else if(exceptionType == typeof(UnauthorizedException))
            {
                message = "Unauthorized";
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = "Internal server error";
            }
            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
