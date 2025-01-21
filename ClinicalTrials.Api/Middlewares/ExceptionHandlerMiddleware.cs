using System.Net;
using System.Text.Json;

namespace ClinicalTrials.Api.Middlewares
{
    //TODO
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //TODO
            var httpStatusCode = HttpStatusCode.InternalServerError;
            string result = string.Empty;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            if (result.Length == 0)
            {
                result = JsonSerializer.Serialize(new
                {
                    error = exception.Message
                });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
