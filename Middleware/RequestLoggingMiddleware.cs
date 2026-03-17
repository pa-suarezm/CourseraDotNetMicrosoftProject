using System.Net;
using System.Text.Json;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log Request
            _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
            
            await _next(context);

            // Log Response
            _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
        }
    }
}