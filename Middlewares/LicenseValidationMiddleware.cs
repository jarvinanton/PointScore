using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PointScore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;

namespace PointScore.Middlewares
{
    public class LicenseValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ILogger<LicenseValidationMiddleware> _logger;

        public LicenseValidationMiddleware(RequestDelegate next, IConfiguration config, ILogger<LicenseValidationMiddleware> logger)
        {
            _next = next;
            _config = config;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ILicenseService licenseService)
        {
            var path = context.Request.Path.Value ?? "";
            
            // Allow Swagger, favicon, health and license management endpoints
            if (path == "/" || path.StartsWith("/swagger") || path == "/favicon.ico" || path == "/health" ||
                path.StartsWith("/api/licenses"))
            {
                await _next(context);
                return;
            }

            // Extract UserId from the authenticated User
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"User is not authenticated or user ID is missing.\"}");
                return;
            }

            // Check if the user has an active license
            if (!await licenseService.IsUserLicensedAsync(userId))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"A premium license is required to access this resource.\"}");
                return;
            }

            await _next(context);
        }
    }
}
