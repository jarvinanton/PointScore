using System.Threading.Tasks;

namespace PointScore.Services

{
    public interface ILicenseService
    {
        Task<bool> IsValidAsync(string licenseKey);
        Task IncrementUsageAsync(string licenseKey);
        Task<bool> ExistsAsync(string licenseId); 
    }
}