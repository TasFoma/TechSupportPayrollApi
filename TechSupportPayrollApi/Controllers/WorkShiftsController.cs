using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShiftsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkShifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkShift>>> GetWorkShifts()
        {
            return await _context.WorkShifts
                .Include(ws => ws.Employee)  // Включаем данные о сотруднике
                .ToListAsync();
        }

        // GET: api/WorkShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkShift>> GetWorkShift(int id)
        {
            var workShift = await _context.WorkShifts
                .Include(ws => ws.Employee)  // Включаем данные о сотруднике
                .FirstOrDefaultAsync(ws => ws.Id == id);

            if (workShift == null)
            {
                return NotFound();
            }

            return workShift;
        }

        // POST: api/WorkShifts/start
        [HttpPost("start")]
        public async Task<ActionResult<WorkShift>> StartShift([FromBody] StartShiftRequest request)
        {
            // Проверяем существование сотрудника
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            if (employee == null)
            {
                return BadRequest("Сотрудник не найден");
            }

            var shift = new WorkShift
            {
                EmployeeId = request.EmployeeId,
                StartDate = DateTime.UtcNow
            };

            _context.WorkShifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkShift", new { id = shift.Id }, shift);
        }

        // PUT: api/WorkShifts/5/end
        [HttpPut("{id}/end")]
        public async Task<IActionResult> EndShift(int id)
        {
            var shift = await _context.WorkShifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            if (shift.EndDate != null)
            {
                return BadRequest("Смена уже завершена");
            }

            shift.EndDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/WorkShifts/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<WorkShift>>> GetEmployeeShifts(int employeeId)
        {
            return await _context.WorkShifts
                .Where(ws => ws.EmployeeId == employeeId)
                .Include(ws => ws.Employee)
                .ToListAsync();
        }

        // DELETE: api/WorkShifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkShift(int id)
        {
            var workShift = await _context.WorkShifts.FindAsync(id);
            if (workShift == null)
            {
                return NotFound();
            }

            _context.WorkShifts.Remove(workShift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkShiftExists(int id)
        {
            return _context.WorkShifts.Any(e => e.Id == id);
        }
    }

    public class StartShiftRequest
    {
        public int EmployeeId { get; set; }
    }
}