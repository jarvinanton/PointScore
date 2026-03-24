using PointScore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PointScore.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly CoreDbContext _db;

        public LicenseService(CoreDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsValidAsync(string licenseKey)
        {
            var license = await _db.Licenses.FirstOrDefaultAsync(l => l.Key == licenseKey);
            if (license == null || !string.Equals(license.Status, "Active", StringComparison.OrdinalIgnoreCase))
                return false;

            // Validar límite de requests mensual
            var currentMonth = DateTime.UtcNow.ToString("yyyy-MM");
            if (license.MonthKey != currentMonth)
            {
                license.MonthKey = currentMonth;
                license.CurrentMonthRequests = 0;
                await _db.SaveChangesAsync();
            }

            return true;
        }

        public async Task IncrementUsageAsync(string licenseKey)
        {
            var license = await _db.Licenses.FirstOrDefaultAsync(l => l.Key == licenseKey);
            if (license != null)
            {
                license.CurrentMonthRequests++;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string licenseId)
        {
            if (!int.TryParse(licenseId, out var id))
                return false;

            return await _db.Licenses.AnyAsync(l => l.Id == id && l.Status.ToLower() == "active");
        }
    }
}