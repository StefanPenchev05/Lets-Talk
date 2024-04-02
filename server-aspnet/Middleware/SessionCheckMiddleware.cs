using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Middleware
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the session contains the user ID
            if (string.IsNullOrEmpty(context.Session.GetString("UserId")))
            {
                // If not, return a 400 Bad Request response
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("User ID is missing from the session.");
                return;
            }

            // If the session contains the user ID, call the next middleware in the pipeline
            await _next(context);
        }
    }
}