using Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // passa para o próximo middleware
            }
            catch (Exception ex)
            {
                LogException(context, ex);

                var result = new
                {
                    status = context.Response.StatusCode,
                    message = ex.Message
                };

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                CustomException custom => custom.StatusCode,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                StatusCode = statusCode,
                exception.Message
            };

            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(jsonResponse);
        }

        private void LogException(HttpContext context, Exception ex)
        {
            var path = context.Request.Path.Value;
            var method = context.Request.Method;
            var traceId = context.TraceIdentifier;

            // Exceções de domínio → Warning
            if (ex is CustomException || ex is ArgumentException)
            {
                _logger.LogWarning(
                    "Falha de negócio em {Method} {Path}. Motivo: {Message}. TraceId={TraceId}",
                    method,
                    path,
                    ex.Message,
                    traceId
                );
                return;
            }

            // Exceções técnicas → Error (sem stack trace no log principal)
            _logger.LogError(
                "Erro interno em {Method} {Path}. TraceId={TraceId}",
                method,
                path,
                traceId
            );
        }
    }
}
