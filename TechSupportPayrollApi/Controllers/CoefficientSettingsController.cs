using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoefficientSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoefficientSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CoefficientSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoefficientSetting>>> GetCoefficientSettings()
        {
            return await _context.CoefficientSettings.ToListAsync();
        }

        // GET: api/CoefficientSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoefficientSetting>> GetCoefficientSetting(int id)
        {
            var coefficientSetting = await _context.CoefficientSettings.FindAsync(id);

            if (coefficientSetting == null)
            {
                return NotFound();
            }

            return coefficientSetting;
        }

        // POST: api/CoefficientSettings
        [HttpPost]
        public async Task<ActionResult<CoefficientSetting>> PostCoefficientSetting(CoefficientSetting coefficientSetting)
        {
            _context.CoefficientSettings.Add(coefficientSetting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoefficientSetting", new { id = coefficientSetting.Id }, coefficientSetting);
        }

        // PUT: api/CoefficientSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoefficientSetting(int id, CoefficientSetting coefficientSetting)
        {
            if (id != coefficientSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(coefficientSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoefficientSettingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/CoefficientSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoefficientSetting(int id)
        {
            var coefficientSetting = await _context.CoefficientSettings.FindAsync(id);
            if (coefficientSetting == null)
            {
                return NotFound();
            }

            _context.CoefficientSettings.Remove(coefficientSetting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoefficientSettingExists(int id)
        {
            return _context.CoefficientSettings.Any(e => e.Id == id);
        }
    }
}