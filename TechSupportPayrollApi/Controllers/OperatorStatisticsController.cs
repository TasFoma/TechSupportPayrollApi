using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupportPayrollApi.Data;
using TechSupportPayrollApi.Data.Entities;

namespace TechSupportPayrollApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorStatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OperatorStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OperatorStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperatorStatistic>>> GetOperatorStatistics()
        {
            return await _context.OperatorStatistics
                .Include(os => os.Employee)
                .Include(os => os.Shift)  
                .ToListAsync();
        }
         
        // GET: api/OperatorStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperatorStatistic>> GetOperatorStatistic(int id)
        {
            var operatorStatistic = await _context.OperatorStatistics
                .Include(os => os.Employee)
                 .Include(os => os.Shift)  
                .FirstOrDefaultAsync(os => os.Id == id);

            if (operatorStatistic == null)
            {
                return NotFound();
            }

            return operatorStatistic;
        }

        // POST: api/OperatorStatistics
        [HttpPost]
        public async Task<ActionResult<OperatorStatistic>> PostOperatorStatistic(OperatorStatistic operatorStatistic)
        {
            _context.OperatorStatistics.Add(operatorStatistic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperatorStatistic", new { id = operatorStatistic.Id }, operatorStatistic);
        }

        // GET: api/OperatorStatistics/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<OperatorStatistic>>> GetEmployeeStatistics(int employeeId)
        {
            return await _context.OperatorStatistics
                .Include(os => os.Employee)
                .Where(os => os.EmployeeId == employeeId)
                .ToListAsync();
        }

        // DELETE: api/OperatorStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperatorStatistic(int id)
        {
            var operatorStatistic = await _context.OperatorStatistics.FindAsync(id);
            if (operatorStatistic == null)
            {
                return NotFound();
            }

            _context.OperatorStatistics.Remove(operatorStatistic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperatorStatisticExists(int id)
        {
            return _context.OperatorStatistics.Any(e => e.Id == id);
        }
    }
}