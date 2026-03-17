using System.Net;

namespace UserManagementAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string RequiredToken = "TechHive-Secret-Key"; // Simplified for this example

        public AuthMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var token) || token != RequiredToken)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid or missing token.");
                return;
            }
            await _next(context);
        }
    }
}