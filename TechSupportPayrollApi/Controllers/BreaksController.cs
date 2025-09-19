using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreaksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BreaksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Breaks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Break>>> GetBreaks()
        {
            return await _context.Breaks
                .Include(b => b.Shift) 
                .ThenInclude(ws => ws.Employee)
                .ToListAsync();
        }

        // GET: api/Breaks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Break>> GetBreak(int id)
        {
            var @break = await _context.Breaks
                .Include(b => b.Shift)  
                .ThenInclude(ws => ws.Employee)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (@break == null)
            {
                return NotFound();
            }

            return @break;
        }
        [HttpGet("shift/{shiftId}")]
        public async Task<ActionResult<IEnumerable<Break>>> GetBreaksForShift(int shiftId)
        {
            return await _context.Breaks
                .Where(b => b.ShiftId == shiftId)
                .Include(b => b.Employee)
                .ToListAsync();
        }
        // POST: api/Breaks/start
        [HttpPost("start")]
        public async Task<ActionResult<Break>> StartBreak([FromBody] StartBreakRequest request)
        {
            // Проверяем, что смена существует и не завершена
            var shift = await _context.WorkShifts
                .FirstOrDefaultAsync(ws => ws.Id == request.ShiftId && ws.EndDate == null);  

            if (shift == null)
            {
                return BadRequest("Смена не найдена или уже завершена");
            }

            var @break = new Break
            {
                ShiftId = request.ShiftId, 
                EmployeeId = shift.EmployeeId, // Добавляем EmployeeId из смены
                StartDate = DateTime.UtcNow
            };

            _context.Breaks.Add(@break);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBreak", new { id = @break.Id }, @break);
        }

        // PUT: api/Breaks/5/end
        [HttpPut("{id}/end")]
        public async Task<IActionResult> EndBreak(int id)
        {
            var @break = await _context.Breaks.FindAsync(id);
            if (@break == null)
            {
                return NotFound();
            }

            if (@break.EndDate != null)
            {
                return BadRequest("Перерыв уже завершен");
            }

            @break.EndDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Breaks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreak(int id)
        {
            var @break = await _context.Breaks.FindAsync(id);
            if (@break == null)
            {
                return NotFound();
            }

            _context.Breaks.Remove(@break);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BreakExists(int id)
        {
            return _context.Breaks.Any(e => e.Id == id);
        }
    }

    public class StartBreakRequest
    {
        public int ShiftId { get; set; } // 
    }
}