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
            if (path.StartsWith("/swagger") || path == "/favicon.ico" || path == "/health" ||
                path.StartsWith("/api/licenses"))
            {
                await _next(context);
                return;
            }

            var licenseToken = context.Request.Headers["X-License-Token"].FirstOrDefault();
            if (string.IsNullOrEmpty(licenseToken))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Missing or invalid X-License-Token header.");
                return;
            }

            var token = licenseToken.Trim();

            try
            {
                var signingKey = _config["LicenseTokens:SigningKey"];
                if (string.IsNullOrWhiteSpace(signingKey))
                    throw new InvalidOperationException("LicenseTokens:SigningKey is missing");

                var keyBytes = Convert.FromBase64String(signingKey);
                var tokenHandler = new JwtSecurityTokenHandler();

                var issuer = _config["LicenseTokens:Issuer"];
                var audience = _config["LicenseTokens:Audience"];

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var licenseIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "lic.id")?.Value;

                if (licenseIdClaim == null || !await licenseService.ExistsAsync(licenseIdClaim))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("License not found or expired.");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid or expired license token");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid or expired license token.");
                return;
            }

            await _next(context);
        }
    }
}
