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

        public LicensesController(CoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<License>>> GetAll()
        {
            return await _db.Licenses.Include(l => l.User).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<License>> Get(int id)
        {
            var license = await _db.Licenses.Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
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
                UserId = request.UserId,
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
    }
}
