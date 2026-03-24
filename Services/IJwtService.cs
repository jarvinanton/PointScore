using System;
using PointScore.Models;

namespace PointScore.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string name, string[] roles, string[] permissions);
        string GenerateLicenseToken(License license);
    }
}
