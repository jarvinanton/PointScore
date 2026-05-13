using System.Threading.Tasks;
using System;

namespace PointScore.Services
{
    public interface ILicenseService
    {
        Task<bool> IsValidAsync(string licenseKey);
        Task<bool> IsUserLicensedAsync(Guid userId);
        Task IncrementUsageAsync(string licenseKey);
        Task<bool> ExistsAsync(string licenseId); 
    }
}