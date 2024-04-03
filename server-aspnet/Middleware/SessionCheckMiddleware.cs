using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Middleware
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SessionCheckMiddleware> _logger;

        public SessionCheckMiddleware(RequestDelegate next, ILogger<SessionCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the session contains the user ID
            if (string.IsNullOrEmpty(context.Session.GetString("UserId")))
            {
                // If not, return a 400 Bad Request response
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("User ID is missing from the session.");
            }
            else
            {
                // If the session contains the user ID, log it
                _logger.LogInformation("User ID: {UserId}", context.Session.GetString("UserId"));
            }

            // Always call the next middleware in the pipeline
            await _next(context);
        }
    }
}