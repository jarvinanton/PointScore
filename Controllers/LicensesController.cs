using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointScore.Data;
using PointScore.Models;
using PointScore.Models.DTOs;
using PointScore.Services;
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;

namespace PointScore.Controllers
{
    [ApiController]
    [Route("api/licenses")]
    [Authorize(Roles = "Admin")]
    public class LicensesController : ControllerBase
    {
        private readonly CoreDbContext _db;
        private readonly IJwtService _jwt;

        public LicensesController(CoreDbContext db, IJwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<License>>> GetAll()
        {
            return await _db.Licenses.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<License>> Get(int id)
        {
            var license = await _db.Licenses.FindAsync(id);
            if (license == null) return NotFound();
            return license;
        }

        [HttpPost]
        public async Task<ActionResult<License>> Create([FromBody] LicenseCreateDto request)
        {
            if (request == null) return BadRequest("Invalid request payload.");

            var license = new License
            {
                Key = request.Key,
                Expiration = request.Expiration,
                Status = request.Status,
                MonthKey = DateTime.UtcNow.ToString("yyyy-MM"),
                CurrentMonthRequests = 0
            };

            _db.Licenses.Add(license);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = license.Id }, license);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var license = await _db.Licenses.FindAsync(id);
            if (license == null) return NotFound();

            _db.Licenses.Remove(license);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id:int}/token")]
        public async Task<IActionResult> IssueToken(int id)
        {
            var lic = await _db.Licenses.FirstOrDefaultAsync(x => x.Id == id);
            if (lic == null) return NotFound("License not found");

            if (!string.Equals(lic.Status, "Active", StringComparison.OrdinalIgnoreCase)) return BadRequest("License not active");
            if (lic.Expiration <= DateTime.UtcNow) return BadRequest("License expired");

            var token = _jwt.GenerateLicenseToken(lic);
            return Ok(new { token });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyLicense([FromBody] LicenseValidationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Key)) return BadRequest("License key is required");

            var lic = await _db.Licenses.FirstOrDefaultAsync(x => x.Key == request.Key);
            if (lic == null) return NotFound("License key not found");

            if (!string.Equals(lic.Status, "Active", StringComparison.OrdinalIgnoreCase)) return BadRequest("License is not active");
            if (lic.Expiration <= DateTime.UtcNow) return BadRequest("License has expired");

            var token = _jwt.GenerateLicenseToken(lic);
            return Ok(new { token });
        }
    }
}
